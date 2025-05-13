using ERP.DataAccess.Domains;

namespace ERP.Infrastructure.Interfaces
{
    public interface IBorderRepository : IGenericRepository<Border>
    {
        void Update(Border border);

        void UpdateRange(IEnumerable<Border> borders);
    }
}