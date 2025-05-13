using ERP.DataAccess.DBContext;
using ERP.Infrastructure.Interfaces;

namespace ERP.Repositories.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IOrganizationRepository OrganizationRepository { get; private set; }
        public IBankRepository BankRepository { get; private set; }
        public IBankBranchRepository BankBranchRepository { get; private set; }
        public IOrganizationBankAccountRepository OrganizationBankAccountRepository { get; private set; }
        public ILCFileRepository LCFileRepository { get; private set; }
        public IBorderRepository BorderRepository { get; private set; }
        public ICNFCompanyRepository CNFCompanyRepository { get; private set; }
        public ICollectionModeRepository CollectionModeRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            ApplicationUser = new ApplicationUserRepository(_dbContext);
            OrganizationRepository = new OrganizationRepository(_dbContext);
            BankRepository = new BankRepository(_dbContext);
            BankBranchRepository = new BankBranchRepository(_dbContext);
            OrganizationBankAccountRepository = new OrganizationBankAccountRepository(_dbContext);
            LCFileRepository = new LCFileRepository(_dbContext);
            BorderRepository = new BorderRepository(_dbContext);
            CNFCompanyRepository = new CNFCompanyRepository(_dbContext);
            CollectionModeRepository = new CollectionModeRepository(_dbContext);
        }

        public async Task CommitAsync()
       => await _dbContext.SaveChangesAsync();

        public async Task RollbackAsync()
        => await _dbContext.DisposeAsync();
    }
}