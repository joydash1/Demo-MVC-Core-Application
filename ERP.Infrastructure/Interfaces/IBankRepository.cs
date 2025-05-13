using ERP.DataAccess.Domains;

namespace ERP.Infrastructure.Interfaces
{
    public interface IBankRepository : IGenericRepository<Bank>
    {
        void Update(Bank bank);

        void UpdateRange(IEnumerable<Bank> banks);
    }
}