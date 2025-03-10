using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Interfaces
{
    public interface ISpService
    {
        Task<IEnumerable<T>> GetDataWithParameterAsync<T>(object parameters, string storedProcedureName) where T : class;
        Task<IEnumerable<T>> GetDataWithoutParameterAsync<T>(string storedProcedureName) where T : class;
        Task<IEnumerable<T>> GetDataBySqlCommandAsync<T>(string sqlString) where T : class;
        Task<int> ExecuteStoredProcedureAsync(object parameters, string storedProcedureName);
    }
}
