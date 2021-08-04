using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        private readonly DbFactory _dbFactory;
        public StoreRepository(DbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<IQueryable<Store>> Get(bool includeDeleted = false)
        {
            return this.List(c => (includeDeleted || !c.IsDeleted))
                //.Include(c => c.Type)
                .Include(c => c.ShoppingCenter)
                .Include(c => c.Floor)
                .Include(c => c.Category);
        }

        public Task<Store> GetById(int id, bool includeDeleted = false)
        {
            return this.DbSet
                .Where(c => c.Id == id && (includeDeleted || !c.IsDeleted))
                //.Include(c => c.Type)
                .Include(c => c.ShoppingCenter)
                .Include(c => c.Floor)
                .Include(c => c.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
