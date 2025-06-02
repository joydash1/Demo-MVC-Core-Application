using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Domains
{
    public class StockDetails
    {
        [Key]
        public int Id { get; set; }

        public int StockMasterId { get; set; }

        public decimal? QuantityTon { get; set; }

        public decimal? QuantityKg { get; set; }

        public int SlotNo { get; set; }

        public int CNFPaymentId { get; set; }
    }
}