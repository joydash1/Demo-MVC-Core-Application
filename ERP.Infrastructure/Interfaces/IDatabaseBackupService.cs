namespace ERP.Infrastructure.Interfaces
{
    public interface IDatabaseBackupService
    {
        Task<bool> BackupDatabaseAsync(string backupPath);
    }
}