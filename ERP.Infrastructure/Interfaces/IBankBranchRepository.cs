using ERP.DataAccess.Domains;

namespace ERP.Infrastructure.Interfaces
{
    public interface IBankBranchRepository : IGenericRepository<BankBranch>
    {
        void Update(BankBranch bankBranch);

        void UpdateRange(IEnumerable<BankBranch> bankBranches);
    }
}