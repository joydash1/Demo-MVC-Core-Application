using System.ComponentModel.DataAnnotations;

namespace ERP.DataAccess.Domains
{
    public class LCTransaction
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public DateTime OpeningDate { get; set; }

        [Required]
        public int OrganizationBankId { get; set; }

        [Required]
        public int OrganizationBranchId { get; set; }

        [Required]
        public string OrganizationAccountNo { get; set; }

        [Required]
        public int InsuranceId { get; set; }

        [Required]
        [StringLength(50)]
        public string LCNumber { get; set; }

        [Required]
        public decimal ProductWeightTon { get; set; }

        [Required]
        public decimal AmountPerTon { get; set; }

        [Required]
        public decimal TotalLCAmount { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int BorderId { get; set; }

        [Required]
        public int ImporterId { get; set; }

        [Required]
        public int ExporterId { get; set; }

        [Required]
        public DateTime EntryDate { get; set; }

        [Required]
        public int EntryUserId { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateUserId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public decimal MainLcWeightTon { get; set; }
    }
}