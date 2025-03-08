using ERP.DataAccess.DBContext;
using ERP.DataAccess.Domains;
using ERP.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Repositories.Services
{
    internal class ApplicationUserRepository : GenericRepository<ApplicationUser>, IApplicationUserRepository
    {
        protected readonly ApplicationDbContext _userDb;

        public ApplicationUserRepository(ApplicationDbContext userDb) : base(userDb)
        {
            _userDb = userDb;
        }

        public void Update(ApplicationUser applicationUser)
        => _userDb.ApplicationUser.Update(applicationUser);

        public void UpdateRange(IEnumerable<ApplicationUser> applicationUsers)
        => _userDb.ApplicationUser.UpdateRange(applicationUsers);
    }
}
