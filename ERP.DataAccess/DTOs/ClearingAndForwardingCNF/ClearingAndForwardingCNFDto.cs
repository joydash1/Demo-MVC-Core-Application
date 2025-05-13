using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.DTOs.ClearingAndFordwingCNF
{
    public class ClearingAndForwardingCNFDto
    {
        public int Id { get; set; }
        public required int LCId { get; set; }
        public required int CNFCompanyId { get; set; }
        public required decimal CNFWeight { get; set; }
        public string? LCNumber { get; set; }
        public  string? CompanyName { get; set; }
        public  string? CNFDate { get; set; }
        public  string? BorderName { get; set; }
        public  decimal? TotalCNFWeightByLC { get; set; }

    }
}
