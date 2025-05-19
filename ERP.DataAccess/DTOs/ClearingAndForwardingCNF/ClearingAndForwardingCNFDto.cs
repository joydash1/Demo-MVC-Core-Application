using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.DataAccess.DTOs.ClearingAndFordwingCNF
{
    public class ClearingAndForwardingCNFDto
    {
        public int Id { get; set; }
        public int LCId { get; set; }
        public int CNFCompanyId { get; set; }
        public decimal CNFWeight { get; set; }
        public decimal CNFAmount { get; set; }
        public string CNFDocumentFile { get; set; }
        public int? IsFullPaid { get; set; }
        public string? LCNumber { get; set; }
        public string? CompanyName { get; set; }
        public string? CNFDate { get; set; }
        public string? BorderName { get; set; }
        public decimal? TotalCNFWeightByLC { get; set; }
        public decimal? TotalCNFAmountByLC { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }
    }
}