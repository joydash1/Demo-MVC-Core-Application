using System.ComponentModel.DataAnnotations;

namespace ERP.DataAccess.Domains
{
    public class OrganizationBankAccount
    {
        [Key]
        public int ID { get; set; }

        public int OrganizationID { get; set; }
        public int BankId { get; set; }
        public int BranchId { get; set; }
        public string BankAccountNo { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDate { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
    }
}