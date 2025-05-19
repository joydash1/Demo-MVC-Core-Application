using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.DTOs.ClearingAndForwardingCNF
{
    public class NonPaidCNFPaymentListDtos
    {
        public int Id { get; set; }
        public int LCId { get; set; }
        public decimal CNFWeight { get; set; }
        public decimal CNFAmount { get; set; }
        public string CnfDate { get; set; }
        public string LCNumber { get; set; }
    }
}
