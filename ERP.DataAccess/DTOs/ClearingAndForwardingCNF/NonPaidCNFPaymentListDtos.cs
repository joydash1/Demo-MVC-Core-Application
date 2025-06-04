using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.DTOs.ClearingAndForwardingCNF
{
    public class NonPaidCNFPaymentListDtos
    {
        public int ID { get; set; }
        public string LCNumber { get; set; }
        public string ProductName { get; set; }
        public decimal CNFWeight { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }
    }
}
