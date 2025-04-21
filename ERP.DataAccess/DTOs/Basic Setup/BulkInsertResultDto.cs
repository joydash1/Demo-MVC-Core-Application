using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.DTOs.Basic_Setup
{
    public class BulkInsertResultDto
    {
        public bool Success { get; set; }
        public int RecordsProcessed { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
