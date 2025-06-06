using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.DTOs.ProductStock
{
    public class ProductStockByIdDto
    {
        public int Id { get; set; }
        public int LCId { get; set; }
        public decimal TotalStockInQuantityTon { get; set; }
        public decimal TotalStockInQuantityKg { get; set; }
        public decimal StockPricePerKg { get; set; }
        public decimal StockTotalPriceTk { get; set; }
    }
}