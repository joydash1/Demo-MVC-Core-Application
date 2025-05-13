using System.ComponentModel.DataAnnotations;

namespace ERP.DataAccess.Domains
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        public string CategoryName { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDate { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
    }
}