using Dapper;
using ERP.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ERP.Repositories.Services
{
    public class SpService : ISpService
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public SpService(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DBcontext");
        }

        public async Task<IEnumerable<T>> GetDataWithParameterAsync<T>(object parameters, string storedProcedureName) where T : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<T>> GetDataWithoutParameterAsync<T>(string storedProcedureName) where T : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<T>(storedProcedureName, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<T>> GetDataBySqlCommandAsync<T>(string sqlString) where T : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<T>(sqlString, commandType: CommandType.Text);
            }
        }

        public async Task<int> ExecuteStoredProcedureAsync(object parameters, string storedProcedureName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}