using ERP.DataAccess.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Interfaces
{
    public interface ICollectionModeRepository : IGenericRepository<CollectionMode>
    {
        void Update(CollectionMode collectionMode);
        void UpdateRange(IEnumerable<CollectionMode> collectionModes);
    }
}
