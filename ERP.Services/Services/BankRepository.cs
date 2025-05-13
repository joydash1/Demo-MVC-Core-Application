using ERP.DataAccess.DBContext;
using ERP.DataAccess.Domains;
using ERP.Infrastructure.Interfaces;

namespace ERP.Repositories.Services
{
    public class BankRepository : GenericRepository<Bank>, IBankRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BankRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Bank bank)
        => _dbContext.Bank.Update(bank);

        public void UpdateRange(IEnumerable<Bank> banks)
        => _dbContext.Bank.UpdateRange(banks);
    }
}