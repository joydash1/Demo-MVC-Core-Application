using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUser { get; }
        IOrganizationRepository OrganizationRepository { get;}
        IBankRepository BankRepository { get;}
        IBankBranchRepository BankBranchRepository { get;}
        IOrganizationBankAccountRepository OrganizationBankAccountRepository { get;}
        ILCFileRepository LCFileRepository { get;}
        Task CommitAsync();
        Task RollbackAsync();
    }
}
