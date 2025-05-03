using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Domains
{
    public class CustomDataRecordAndStock
    {
        [Key]
        public int ID { get; set; }
        public required int LcId { get; set; }
        public required decimal USDRateToTaka { get; set; }
        public required string TruckNumber { get; set; }
        public required decimal TotalProductWeightKg { get; set; }
        public required decimal ProductPricePerKg { get; set; }
        public required decimal TotalAmount { get; set; }
        public  decimal TotalPaidAmount { get; set; }
        public  decimal TotalDueAmount { get; set; }
        public required int EntryUserId { get; set; }
        public required DateTime EntryDate { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public required bool IsActive { get; set; }
    }
}
