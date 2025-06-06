using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Domains
{
    public class ProductStockMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int LCId { get; set; }

        [Required]
        public decimal TotalStockInQuantityTon { get; set; }

        public decimal? TotalStockOutQuantityTon { get; set; }

        [Required]
        public decimal TotalStockInQuantityKg { get; set; }

        public decimal? TotalStockOutQuantityKg { get; set; }

        [Required]
        public decimal StockPricePerKg { get; set; }

        [Required]
        public decimal StockTotalPriceTk { get; set; }

        public int EntryUserId { get; set; }
        public DateTime EntryDate { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
    }
}