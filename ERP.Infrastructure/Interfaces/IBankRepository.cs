using ERP.DataAccess.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Interfaces
{
    public interface IBankRepository : IGenericRepository<Bank>
    {
        void Update(Bank bank);
        void UpdateRange(IEnumerable<Bank> banks);
    }
}
