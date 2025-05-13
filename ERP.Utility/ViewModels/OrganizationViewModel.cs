using ERP.DataAccess.Domains;

namespace ERP.Utility.ViewModels
{
    public class OrganizationViewModel
    {
        public Organization Organization { get; set; } //= new Organization();
        public IEnumerable<Organization> OrganizationList { get; set; } = new List<Organization>();
    }
}