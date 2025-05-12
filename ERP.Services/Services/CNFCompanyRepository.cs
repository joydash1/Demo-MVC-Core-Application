using ERP.DataAccess.DBContext;
using ERP.DataAccess.Domains;
using ERP.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Repositories.Services
{
    public class CNFCompanyRepository : GenericRepository<CNFCompnay>, ICNFCompanyRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CNFCompanyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(CNFCompnay cNFCompnay)
        => _dbContext.CNFCompnay.Update(cNFCompnay);

        public void UpdateRange(IEnumerable<CNFCompnay> cNFCompnays)
        => _dbContext.CNFCompnay.UpdateRange(cNFCompnays);
    }
}
