using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Domains
{
    public class StockMaster
    {
        [Key]
        public int Id { get; set; }

        public int LCId { get; set; }

        public int CNFId { get; set; }

        public decimal? TotalQuantityTon { get; set; }

        public decimal? TotalQuantityKg { get; set; }

        public decimal? InQuantityTon { get; set; }

        public decimal? InQuantityKg { get; set; }

        public decimal? OutQuantityTon { get; set; }

        public decimal? OutQuantityKg { get; set; }
    }
}