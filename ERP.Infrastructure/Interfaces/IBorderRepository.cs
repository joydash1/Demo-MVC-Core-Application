using ERP.DataAccess.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Interfaces
{
    public interface IBorderRepository : IGenericRepository<Border>
    {
        void Update(Border border);
        void UpdateRange(IEnumerable<Border> borders);
    }
}
