using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.DTOs.ProductStock
{
    public record class ProductStockMasterDto
    (
        int Id,
        int LCId,
        decimal TotalStockInQuantityTon,
        //decimal? TotalStockOutQuantityTon,
        decimal TotalStockInQuantityKg,
        //decimal? TotalStockOutQuantityKg,
        decimal StockPricePerKg,
        decimal StockTotalPriceTk
    );
}