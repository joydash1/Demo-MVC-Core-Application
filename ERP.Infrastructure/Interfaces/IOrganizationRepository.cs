using ERP.DataAccess.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Interfaces
{
    public interface IOrganizationRepository : IGenericRepository<Organization>
    {
        void Update(Organization organization);
        void UpdateRange(IEnumerable<Organization> organizations);
    }
}
