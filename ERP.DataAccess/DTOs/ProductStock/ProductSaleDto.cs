using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.DTOs.ProductStock
{
    public record  ProductSaleDto
    (
        int ProductStockMasterId,
        decimal QuantityKg,
        int SellerId,
        decimal TotalAmount,
        string SaleDate
    );
}
