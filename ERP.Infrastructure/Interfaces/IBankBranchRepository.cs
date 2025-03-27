using ERP.DataAccess.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Interfaces
{
    public interface IBankBranchRepository : IGenericRepository<BankBranch>
    {
        void Update(BankBranch bankBranch);
        void UpdateRange(IEnumerable<BankBranch> bankBranches);
    }
}
