using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Domains
{
    public class CustomDataRecordAndStockBilling
    {
        [Key]
        public int ID { get; set; }
        public required string ExpenseDetails { get; set; }
        public required decimal ExpenseAmount { get; set; }
        public required int CMFId { get; set; }
        public required int EntryUserId { get; set; }
        public required DateTime EntryDate { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public required bool IsActive { get; set; }
    }
}
