using System.ComponentModel.DataAnnotations;

namespace ERP.DataAccess.Domains
{
    public class ClearingAndForwardingCNF
    {
        [Key]
        public int ID { get; set; }

        public int LCId { get; set; }

        public int CNFCompanyId { get; set; }

        public decimal CNFWeight { get; set; }

        public decimal CNFAmount { get; set; }

        public string CNFDocumentFile { get; set; }

        public int EntryUserId { get; set; }

        public DateTime EntryDate { get; set; }

        public int? UpdateUserId { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsActive { get; set; }
    }
}