using ERP.DataAccess.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Interfaces
{
    public interface ICNFCompanyRepository : IGenericRepository<CNFCompnay>
    {
        void Update(CNFCompnay cNFCompnay);
        void UpdateRange(IEnumerable<CNFCompnay> cNFCompnays);
    }
}
