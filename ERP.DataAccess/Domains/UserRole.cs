using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Domains
{
    public class UserRole
    {
        [Key]
        public int ID { get; set; }
        public required string RoleName{ get; set; }
    }
}
