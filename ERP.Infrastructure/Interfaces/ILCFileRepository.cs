using ERP.DataAccess.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Interfaces
{
    public interface ILCFileRepository : IGenericRepository<LCTransaction>
    {
        void Update(LCTransaction lCTransaction);
        void UpdateRange(IEnumerable<LCTransaction> lCTransactions);
    }
}
