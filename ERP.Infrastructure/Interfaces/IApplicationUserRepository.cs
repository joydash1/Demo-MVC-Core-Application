using ERP.DataAccess.Domains;

namespace ERP.Infrastructure.Interfaces
{
    public interface IApplicationUserRepository : IGenericRepository<ApplicationUser>
    {
        void Update(ApplicationUser applicationUser);

        void UpdateRange(IEnumerable<ApplicationUser> applicationUsers);
    }
}