using ERP.DataAccess.DBContext;
using ERP.DataAccess.Domains;
using ERP.Infrastructure.Interfaces;

namespace ERP.Repositories.Services
{
    public class BorderRepository : GenericRepository<Border>, IBorderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BorderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Border border)
        => _dbContext.Border.Update(border);

        public void UpdateRange(IEnumerable<Border> borders)
        => _dbContext.Border.UpdateRange(borders);
    }
}