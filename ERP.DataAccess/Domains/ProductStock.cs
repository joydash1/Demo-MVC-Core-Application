using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Domains
{
    public class ProductStock
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int LCId { get; set; }

        [Required]
        public decimal ProductWeightKg { get; set; }

        [Required]
        public decimal USDRate { get; set; }

        [Required]
        public decimal TotalProductPrice { get; set; }

        [Required]
        public decimal PricePerkg { get; set; }

        [Required]
        public string TruckNo { get; set; }

        [Required]
        public int TotalBags { get; set; }

        [Required]
        public DateTime EntryDate { get; set; }

        [Required]
        public int EntryUserId { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateUserId { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
