using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.DTOs.LC_Open
{
    public record LCTransactionListDto(int Id, string OpeningDate, string LCNumber,decimal ProductWeightTon, decimal AmountPerTon, decimal TotalLCAmount, string ProductName, string ShopName,decimal ProductWeightKg);
}
