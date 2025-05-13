using System.ComponentModel.DataAnnotations;

namespace ERP.DataAccess.Domains
{
    public class CNFCompnay
    {
        [Key]
        public int Id { get; set; }

        public string CNFCompnayName { get; set; }
        public int BorderId { get; set; }

        public int EntryUserId { get; set; }
        public DateTime EntryDate { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
    }
}