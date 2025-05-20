using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Domains
{
    public class ClearingAndForwardingCNFPaymentSubData
    {
        [Key]
        public int ID { get; set; }

        public int CNFPaymentId { get; set; }
        public int? CollectionModeId { get; set; }
        public int? BankId { get; set; }
        public int? BranchId { get; set; }
        public string? RoutingNo { get; set; }
        public decimal Amount { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDate { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
    }
}
