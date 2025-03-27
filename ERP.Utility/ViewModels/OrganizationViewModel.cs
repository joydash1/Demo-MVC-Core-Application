using ERP.DataAccess.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Utility.ViewModels
{
    public class OrganizationViewModel
    {
        public Organization Organization { get; set; } //= new Organization();
        public IEnumerable<Organization> OrganizationList { get; set; } = new List<Organization>();
    }
}
