using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Interfaces
{
    public interface IDatabaseBackupService
    {
        Task<bool> BackupDatabaseAsync(string backupPath);
    }

}
