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

//string backupFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "DatabaseBackups");
//if (!Directory.Exists(backupFolderPath))
//{
//    Directory.CreateDirectory(backupFolderPath);
//}

//string formattedDate = DateTime.Now.ToString("ddMMMMyyyy");
//string backupFileName = $"{databaseName}{formattedDate}.bak";
//string backupFilePath = Path.Combine(backupFolderPath,backupFileName);

//bool result = await _databaseBackupService.BackupDatabaseAsync(backupFilePath);


//public IActionResult DownloadDatabase()
//{
//    string backupFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "DatabaseBackups");

//    if (!Directory.Exists(backupFolderPath))
//    {
//        Directory.CreateDirectory(backupFolderPath);
//    }
//    var backupFiles = Directory.GetFiles(backupFolderPath, "*.bak")
//                               .Select(Path.GetFileName)
//                               .ToList();

//    ViewBag.BackupFiles = backupFiles;
//    return View();
//}
//[HttpPost]
//public IActionResult DownloadBackup(string fileName)
//{
//    string backupFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "DatabaseBackups");
//    string filePath = Path.Combine(backupFolderPath, fileName);

//    if (System.IO.File.Exists(filePath))
//    {
//        byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
//        return File(fileBytes, "application/octet-stream", fileName);
//    }
//    return NotFound("File not found.");
//}