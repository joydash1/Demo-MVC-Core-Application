﻿using ERP.DataAccess.DBContext;
using ERP.DataAccess.Domains;
using ERP.Infrastructure.Interfaces;

namespace ERP.Repositories.Services
{
    public class CollectionModeRepository : GenericRepository<CollectionMode>, ICollectionModeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CollectionModeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(CollectionMode collectionMode)
        => _dbContext.CollectionMode.Update(collectionMode);

        public void UpdateRange(IEnumerable<CollectionMode> collectionModes)
        => _dbContext.CollectionMode.UpdateRange(collectionModes);
    }
}