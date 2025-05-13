using ERP.DataAccess.DBContext;
using ERP.DataAccess.Domains;
using ERP.Infrastructure.Interfaces;

namespace ERP.Repositories.Services
{
    public class BankBranchRepository : GenericRepository<BankBranch>, IBankBranchRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BankBranchRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(BankBranch bankBranch)
        => _dbContext.BankBranch.Update(bankBranch);

        public void UpdateRange(IEnumerable<BankBranch> bankBranches)
        => _dbContext.BankBranch.UpdateRange(bankBranches);
    }
}