using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Utility.Helpers
{
    public class ReportService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReportService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public FileContentResult ShowReport(DataTable data, string exportType,
                        string reportName, string exportFileName,
                        Dictionary<string, string> reportParameterCollection = null)
        {
            // Validate inputs
            if (data == null || data.Rows.Count == 0)
                throw new ArgumentException("No data found.");

            // Locate report file
            var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", reportName);
            if (!File.Exists(reportPath))
                throw new FileNotFoundException($"Report file not found at path: {reportPath}");

            // Prepare the report
            using var report = new LocalReport();
            report.LoadReportDefinition(File.OpenRead(reportPath));
            report.DataSources.Add(new ReportDataSource("DataSet1", data));

            // Combine static company headers with dynamic parameters
            var allParameters = ReportHelpers.GetCompanyHeader();
            if (reportParameterCollection != null)
            {
                foreach (var param in reportParameterCollection)
                {
                    allParameters[param.Key] = param.Value;
                }
            }

            // Set all parameters
            report.SetParameters(allParameters.Select(p =>
                new ReportParameter(p.Key, p.Value)).ToList());

            // Determine output format
            string mimeType, extension;
            switch (exportType.ToUpper())
            {
                case "EXCEL":
                    mimeType = "application/vnd.ms-excel";
                    extension = "xlsx";
                    break;
                case "WORD":
                    mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    extension = "docx";
                    break;
                default: // Default to PDF
                    mimeType = "application/pdf";
                    extension = "pdf";
                    break;
            }

            // Render the report
            var resultBytes = report.Render(exportType);

            // Set response headers for inline display
            var contentDisposition = new ContentDisposition
            {
                FileName = $"{exportFileName}.{extension}",
                Inline = true
            };

            _httpContextAccessor.HttpContext.Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
            _httpContextAccessor.HttpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");

            return new FileContentResult(resultBytes, mimeType);
        }
        public DataTable ConvertToDataTable<T>(IEnumerable<T> data)
        {
            var table = new DataTable(typeof(T).Name);
            var props = typeof(T).GetProperties();

            foreach (var prop in props)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (var item in data)
            {
                var row = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                    row[i] = props[i].GetValue(item);
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
