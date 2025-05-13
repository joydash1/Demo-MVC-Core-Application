using System.ComponentModel.DataAnnotations;

namespace ERP.DataAccess.Domains
{
    public class BangladeshiSeller
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string SellerName { get; set; }

        [Required]
        [StringLength(200)]
        public string ShopName { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Website { get; set; }

        [StringLength(20)]
        public string TINNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string MobileNo { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public int EntryUserId { get; set; }

        [Required]
        public DateTime EntryDate { get; set; }

        public int? UpdateUserId { get; set; }

        public DateTime? UpdateDate { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}