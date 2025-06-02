using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Domains
{
    public class ProductLedger
    {
        [Key]
        public int Id { get; set; }

        public int LCId { get; set; }

        public int StockMasterId { get; set; }

        public decimal? SellQuantityTon { get; set; }

        public decimal? SellQuantityKg { get; set; }

        public decimal SellAmount { get; set; }

        public DateTime SellDate { get; set; }

        public int CustomerId { get; set; }

        public int CNFId { get; set; }

        public DateTime? DeliveryDate { get; set; }
    }
}