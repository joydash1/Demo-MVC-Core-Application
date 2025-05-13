namespace ERP.DataAccess.DTOs.Basic_Setup
{
    public record ProductDto(int Id, int CategoryId, string ProductName, string? CategoryName);
}