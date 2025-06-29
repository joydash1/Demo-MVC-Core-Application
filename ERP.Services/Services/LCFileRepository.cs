﻿using ERP.DataAccess.DBContext;
using ERP.DataAccess.Domains;
using ERP.Infrastructure.Interfaces;

namespace ERP.Repositories.Services
{
    public class LCFileRepository : GenericRepository<LCTransaction>, ILCFileRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LCFileRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(LCTransaction lCTransaction)
        => _dbContext.LCTransaction.Update(lCTransaction);

        public void UpdateRange(IEnumerable<LCTransaction> lCTransactions)
        => _dbContext.LCTransaction.UpdateRange(lCTransactions);
    }
}