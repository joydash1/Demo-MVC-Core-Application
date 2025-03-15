using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Domains
{
    public class OrganizationBankAccount
    {
        [Key]
        public int ID { get; set; }
        public required int OrganizationID { get; set; }
        public required int BankId { get; set; }
        public required string BranchId { get; set; }
        public required string BankAccountNo { get; set; }
        public required int EntryUserId { get; set; }
        public required DateTime EntryDate { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public required bool IsActive { get; set; }
    }
}
