﻿namespace ERP.DataAccess.DTOs.Product_Stock
{
    public record ProductStockDto(int Id, int lcId, decimal productWeightKg, decimal uSDRate, decimal totalProductPrice, decimal pricePerkg, string truckNo, int totalBags);
}