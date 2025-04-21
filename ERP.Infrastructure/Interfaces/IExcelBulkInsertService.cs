using ERP.DataAccess.DTOs.Basic_Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Interfaces
{
    public interface IExcelBulkInsertService
    {
        Task<BulkInsertResultDto> BulkInsertFromExcelAsync<T>(ExcelImportDto<T> importDto) where T : class;
    }
}
