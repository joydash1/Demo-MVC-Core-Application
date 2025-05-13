using ERP.DataAccess.DBContext;
using ERP.DataAccess.Domains;
using ERP.Infrastructure.Interfaces;

namespace ERP.Repositories.Services
{
    public class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrganizationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Organization organization)
        => _dbContext.Organization.Update(organization);

        public void UpdateRange(IEnumerable<Organization> organizations)
        => _dbContext.Organization.UpdateRange(organizations);
    }
}