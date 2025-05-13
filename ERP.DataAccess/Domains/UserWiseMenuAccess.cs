using System.ComponentModel.DataAnnotations;

namespace ERP.DataAccess.Domains
{
    public class UserWiseMenuAccess
    {
        [Key]
        public int ID { get; set; }

        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string MenuName { get; set; }
        public string? UserIds { get; set; }
    }
}