﻿namespace ERP.DataAccess.DTOs.LC_Open
{
    public record LCTransactionTableDto
        (
            int Id,
            string OpeningDate,
            string LCNumber,
            decimal ProductWeightTon,
            decimal AmountPerTon,
            decimal TotalLCAmount,
            string ProductName,
            string ShopName
       );
}