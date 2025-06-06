using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.DTOs.ClearingAndForwardingCNF
{
    public class CNFPaymentSaveDto
    {
        public int CNFPaymentId { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }
        public string PaymentDate { get; set; }
        public int CollectionModeId { get; set; }
        public int? OrgBankId { get; set; }
        public string? ChequeNo { get; set; }
        public string? ChequeDate { get; set; }
    }
}