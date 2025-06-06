using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Domains
{
    public class ProductSellLedger
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductStockMasterId { get; set; }

        [Required]
        public decimal SellQuantityTon { get; set; }

        [Required]
        public decimal SellQuantityKg { get; set; }

        [Required]
        public decimal SellAmount { get; set; }

        [Required]
        public DateTime SellDate { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public int EntryUserId { get; set; }
        public DateTime EntryDate { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
    }
}