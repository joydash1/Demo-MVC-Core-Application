using ERP.DataAccess.Domains;

namespace ERP.Infrastructure.Interfaces
{
    public interface IOrganizationRepository : IGenericRepository<Organization>
    {
        void Update(Organization organization);

        void UpdateRange(IEnumerable<Organization> organizations);
    }
}