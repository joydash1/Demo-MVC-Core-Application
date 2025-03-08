using ERP.DataAccess.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Interfaces
{
    public interface IApplicationUserRepository : IGenericRepository<ApplicationUser>
    {
        void Update(ApplicationUser applicationUser);
        void UpdateRange(IEnumerable<ApplicationUser> applicationUsers);
    }
}
