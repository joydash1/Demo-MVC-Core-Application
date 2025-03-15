using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Domains
{
    public class UserWiseMenuAccess
    {
        [Key]
        public int ID { get; set; }
        public required string ControllerName { get; set; }
        public required string ActionName { get; set; }
        public required string MenuName { get; set; }
        public string? UserIds { get; set; }
    }
}
