using Microsoft.AspNetCore.Http;

namespace ERP.DataAccess.DTOs.Basic_Setup
{
    public class ExcelImportDto<T> where T : class
    {
        public IFormFile File { get; set; }
        public string SheetName { get; set; } = "Sheet1";
        public bool HasHeaderRow { get; set; } = true;
        public string TableName { get; set; } // Database table name
    }
}