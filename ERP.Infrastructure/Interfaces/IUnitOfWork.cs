namespace ERP.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUser { get; }
        IOrganizationRepository OrganizationRepository { get; }
        IBankRepository BankRepository { get; }
        IBankBranchRepository BankBranchRepository { get; }
        IOrganizationBankAccountRepository OrganizationBankAccountRepository { get; }
        ILCFileRepository LCFileRepository { get; }
        IBorderRepository BorderRepository { get; }
        ICNFCompanyRepository CNFCompanyRepository { get; }
        ICollectionModeRepository CollectionModeRepository { get; }

        Task CommitAsync();

        Task RollbackAsync();
    }
}