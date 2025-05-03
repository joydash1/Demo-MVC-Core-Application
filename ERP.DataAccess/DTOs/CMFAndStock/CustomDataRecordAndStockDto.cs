using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.DTOs.CMF
{
    public record CustomDataRecordAndStockDto
        (
             int Id,
             int LCId,
             decimal TotalProductWeightKg,
             decimal USDRateToTaka,
             decimal TotalProductPrice,
             decimal ProductPricePerKg,
             string TruckNumber
        );


}
