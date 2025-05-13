using System.ComponentModel.DataAnnotations;

namespace ERP.DataAccess.Domains
{
    public class CollectionMode
    {
        [Key]
        public int Id { get; set; }

        public string CollectionModeName { get; set; }
        public int IsActive { get; set; }
    }
}