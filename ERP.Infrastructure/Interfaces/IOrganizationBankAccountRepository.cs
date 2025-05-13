using ERP.DataAccess.Domains;

namespace ERP.Infrastructure.Interfaces
{
    public interface IOrganizationBankAccountRepository : IGenericRepository<OrganizationBankAccount>
    {
        void Update(OrganizationBankAccount organizationBankAccount);

        void UpdateRange(IEnumerable<OrganizationBankAccount> organizationBankAccounts);
    }
}