namespace ERP.DataAccess.DTOs.LC_Open
{
    public record LCTransactionsByIdDto(int Id, string OpeningDate, string LCNumber, decimal ProductWeightTon, decimal AmountPerTon, decimal TotalLCAmount, string ProductName, string ShopName, decimal ProductWeightKg, string OrganizatioName, string? CNFCompnayName);
}