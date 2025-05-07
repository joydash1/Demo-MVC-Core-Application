using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.DTOs.CMFAndStock
{
    //public record CustomDataRecordAndStockListDto(int ID, string ProductName,decimal TotalProductWeightKg, decimal ProductPricePerKg, decimal TotalAmount, decimal TotalDueAmount, decimal TotalPaidAmount, string ShopName);
    public class CustomDataRecordAndStockListDto
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string TotalProductWeightKg { get; set; }
        public string ProductPricePerKg { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDueAmount { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public string ShopName { get; set; }
    }

}
