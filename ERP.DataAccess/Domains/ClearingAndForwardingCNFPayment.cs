using System.ComponentModel.DataAnnotations;

namespace ERP.DataAccess.Domains
{
    public class ClearingAndForwardingCNFPayment
    {
        [Key]
        public int ID { get; set; }

        public int CNFId { get; set; }
        public int CNFCompanyId { get; set; }
        public int? CollectionModeId { get; set; }
        public int? BankId { get; set; }
        public int? BranchId { get; set; }
        public string? RoutingNo { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal? DueAmount { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDate { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
    }
}