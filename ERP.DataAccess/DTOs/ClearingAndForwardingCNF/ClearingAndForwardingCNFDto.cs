namespace ERP.DataAccess.DTOs.ClearingAndFordwingCNF
{
    public class ClearingAndForwardingCNFDto
    {
        public int Id { get; set; }
        public int LCId { get; set; }
        public int CNFCompanyId { get; set; }
        public decimal CNFWeight { get; set; }
        public string? LCNumber { get; set; }
        public string? CompanyName { get; set; }
        public string? CNFDate { get; set; }
        public string? BorderName { get; set; }
        public decimal? TotalCNFWeightByLC { get; set; }
    }
}