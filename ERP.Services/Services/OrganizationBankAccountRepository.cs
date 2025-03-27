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
    public class OrganizationBankAccountRepository : GenericRepository<OrganizationBankAccount>, IOrganizationBankAccountRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrganizationBankAccountRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(OrganizationBankAccount organizationBankAccount)
        => _dbContext.OrganizationBankAccount.Update(organizationBankAccount);

        public void UpdateRange(IEnumerable<OrganizationBankAccount> organizationBankAccounts)
        => _dbContext.OrganizationBankAccount.UpdateRange(organizationBankAccounts);
    }
}
