using System.ComponentModel.DataAnnotations;

namespace ERP.DataAccess.Domains
{
    public class ClearingAndForwardingCNFPayment
    {
        [Key]
        public int ID { get; set; }

        public string CNFId { get; set; }

        public int LCId { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal? DueAmount { get; set; }

        public int EntryUserId { get; set; }

        public DateTime EntryDate { get; set; }

        public int? UpdateUserId { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsActive { get; set; }
    }
}