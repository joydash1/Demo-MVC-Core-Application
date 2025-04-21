using ClosedXML.Excel;
using ERP.DataAccess.DTOs.Basic_Setup;
using ERP.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Repositories.Services
{
    public class ExcelBulkInsertService : IExcelBulkInsertService
    {
        private readonly string _connectionString;
        private readonly ILogger<ExcelBulkInsertService> _logger;

        public ExcelBulkInsertService(IConfiguration configuration, ILogger<ExcelBulkInsertService> logger)
        {
            _connectionString = configuration.GetConnectionString("DBcontext");
            _logger = logger;
        }
        public async Task<BulkInsertResultDto> BulkInsertFromExcelAsync<T>(ExcelImportDto<T> importDto) where T : class
        {
            var result = new BulkInsertResultDto();

            try
            {
                // Validate input
                if (importDto.File == null || importDto.File.Length == 0)
                {
                    result.Errors.Add("No file uploaded.");
                    return result;
                }

                if (string.IsNullOrEmpty(importDto.TableName))
                {
                    result.Errors.Add("Table name must be specified.");
                    return result;
                }

                // Check file extension
                var fileExtension = Path.GetExtension(importDto.File.FileName).ToLower();
                if (fileExtension != ".xlsx" && fileExtension != ".xls")
                {
                    result.Errors.Add("Only Excel files (.xlsx, .xls) are allowed.");
                    return result;
                }

                // Get properties of the model
                var properties = typeof(T).GetProperties();
                var dataTable = CreateDataTableForType<T>();

                // Parse Excel file
                using var stream = new MemoryStream();
                await importDto.File.CopyToAsync(stream);

                using var workbook = new XLWorkbook(stream);
                var worksheet = workbook.Worksheet(importDto.SheetName);

                var rows = importDto.HasHeaderRow ?
                    worksheet.RowsUsed().Skip(1) :
                    worksheet.RowsUsed();

                foreach (var row in rows)
                {
                    try
                    {
                        var dataRow = dataTable.NewRow();

                        for (int i = 0; i < properties.Length; i++)
                        {
                            var property = properties[i];
                            var cellValue = row.Cell(i + 1).Value;

                            try
                            {
                                // Handle null values
                                if (string.IsNullOrWhiteSpace(cellValue.ToString()))
                                {
                                    if (IsNullable(property.PropertyType))
                                    {
                                        dataRow[property.Name] = DBNull.Value;
                                        continue;
                                    }
                                    throw new Exception($"{property.Name} cannot be null");
                                }

                                // Set value based on property type
                                dataRow[property.Name] = ConvertValue(cellValue, property.PropertyType);
                            }
                            catch (Exception ex)
                            {
                                result.Errors.Add($"Row {row.RowNumber()}, Column {property.Name}: {ex.Message}");
                            }
                        }

                        dataTable.Rows.Add(dataRow);
                    }
                    catch (Exception ex)
                    {
                        result.Errors.Add($"Error processing row {row.RowNumber()}: {ex.Message}");
                        _logger.LogError(ex, "Error processing row {RowNumber}", row.RowNumber());
                    }
                }

                // Bulk insert using SqlBulkCopy
                await using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                using var transaction = connection.BeginTransaction();
                try
                {
                    using var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction);
                    bulkCopy.DestinationTableName = importDto.TableName;

                    // Map all columns automatically
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                    }

                    await bulkCopy.WriteToServerAsync(dataTable);
                    await transaction.CommitAsync();

                    result.RecordsProcessed = dataTable.Rows.Count;
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    result.Errors.Add($"Database error: {ex.Message}");
                    _logger.LogError(ex, "Error during bulk insert to table {TableName}", importDto.TableName);
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Bulk insert failed: {ex.Message}");
                _logger.LogError(ex, "Bulk insert failed");
            }

            return result;
        }


        private DataTable CreateDataTableForType<T>() where T : class
        {
            var dataTable = new DataTable();
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                // Get the underlying type if nullable
                var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                // Use ColumnAttribute name if specified
                var columnName = property.GetCustomAttribute<ColumnAttribute>()?.Name ?? property.Name;

                dataTable.Columns.Add(columnName, propertyType);
            }

            return dataTable;
        }

        private object ConvertValue(object cellValue, Type targetType)
        {
            var stringValue = cellValue.ToString();
            var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

            if (underlyingType == typeof(string))
                return stringValue;

            if (underlyingType == typeof(int))
                return int.Parse(stringValue);

            if (underlyingType == typeof(decimal))
                return decimal.Parse(stringValue);

            if (underlyingType == typeof(DateTime))
                return DateTime.Parse(stringValue);

            if (underlyingType == typeof(bool))
                return bool.Parse(stringValue);

            return Convert.ChangeType(stringValue, underlyingType);
        }

        private bool IsNullable(Type type)
        {
            return !type.IsValueType || Nullable.GetUnderlyingType(type) != null;
        }
    }
}
