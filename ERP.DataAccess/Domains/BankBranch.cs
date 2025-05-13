using System.ComponentModel.DataAnnotations;

namespace ERP.DataAccess.Domains
{
    public class BankBranch
    {
        [Key]
        public int ID { get; set; }

        public int BankId { get; set; }
        public string BranchName { get; set; }
        public int RoutingNo { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDate { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
    }
}