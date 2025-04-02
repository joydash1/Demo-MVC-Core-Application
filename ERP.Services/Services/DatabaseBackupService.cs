using ERP.DataAccess.Domains;
using ERP.DataAccess.DTOs.Basic_Setup;
using ERP.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Repositories.Services
{
    public class DatabaseBackupService : IDatabaseBackupService
    {
        private readonly ISpService _spService;
        private readonly IConfiguration _configuration;

        public DatabaseBackupService(ISpService spService, IConfiguration configuration)
        {
            _spService = spService;
            _configuration = configuration;
        }
        public async Task<bool> BackupDatabaseAsync(string backupPath)
        {
            string connectionString = _configuration.GetConnectionString("DBcontext");
            string databaseName = "";

            // Extract Database Name from Connection String
            var builder = new SqlConnectionStringBuilder(connectionString);
            databaseName = builder.InitialCatalog;

            try
            {
                var data = await _spService.GetDataWithParameterAsync<dynamic>(new
                {
                    DATABASE_NAME = databaseName,
                    BACKUP_PATH = backupPath
                }, "USP_DATABASE_BACKUP");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
