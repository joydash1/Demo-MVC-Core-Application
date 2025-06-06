using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.DTOs.ProductStock
{
    public class ProductStockListDto
    {
        public int Id { get; set; }
        public string LCNumber { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal TotalStockInQuantityKg { get; set; }
        public decimal StockPricePerKg { get; set; }
        public decimal StockTotalPriceTk { get; set; }
    }
}