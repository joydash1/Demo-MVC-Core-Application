using ERP.DataAccess.Domains;

namespace ERP.Infrastructure.Interfaces
{
    public interface ICollectionModeRepository : IGenericRepository<CollectionMode>
    {
        void Update(CollectionMode collectionMode);

        void UpdateRange(IEnumerable<CollectionMode> collectionModes);
    }
}