using ERP.DataAccess.DTOs.Basic_Setup;

namespace ERP.Infrastructure.Interfaces
{
    public interface IExcelBulkInsertService
    {
        Task<BulkInsertResultDto> BulkInsertFromExcelAsync<T>(ExcelImportDto<T> importDto) where T : class;
    }
}