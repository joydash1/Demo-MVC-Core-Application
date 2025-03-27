using ERP.DataAccess.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Interfaces
{
    public interface IOrganizationBankAccountRepository : IGenericRepository<OrganizationBankAccount>
    {
        void Update(OrganizationBankAccount organizationBankAccount);
        void UpdateRange(IEnumerable<OrganizationBankAccount> organizationBankAccounts);
    }
}
