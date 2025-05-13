using System.ComponentModel.DataAnnotations;

namespace ERP.DataAccess.Domains
{
    public class UserRole
    {
        [Key]
        public int ID { get; set; }

        public string RoleName { get; set; }
    }
}