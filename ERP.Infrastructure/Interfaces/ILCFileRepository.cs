using ERP.DataAccess.Domains;

namespace ERP.Infrastructure.Interfaces
{
    public interface ILCFileRepository : IGenericRepository<LCTransaction>
    {
        void Update(LCTransaction lCTransaction);

        void UpdateRange(IEnumerable<LCTransaction> lCTransactions);
    }
}