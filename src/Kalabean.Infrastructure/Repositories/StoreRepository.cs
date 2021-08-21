using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.Store;
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

        public async Task<IQueryable<Store>> Get(GetStoresRequest request, bool includeDeleted = false)
        {
            return this.List(c => (includeDeleted || !c.IsDeleted) &&
            (string.IsNullOrEmpty(request.Name) || c.Name.Contains(request.Name)) &&
            (!request.CategoryId.HasValue || request.CategoryId == c.CategoryId) &&
            (!request.IsEnabled.HasValue || request.IsEnabled == c.IsEnabled))
                .Skip(request.PageSize * request.PageIndex).Take(request.PageSize)
                .Include(c => c.Floor)
                .Include(c => c.Category)
                .Include(c => c.ShoppingCenter)
                .ThenInclude(c => c.Type);
        }

        public async Task<int> Count(GetStoresRequest request, bool includeDeleted = false)
        {
            return this.List(c => (includeDeleted || !c.IsDeleted) &&
            (string.IsNullOrEmpty(request.Name) || c.Name.Contains(request.Name)) &&
            (!request.CategoryId.HasValue || request.CategoryId == c.CategoryId) &&
            (!request.IsEnabled.HasValue || request.IsEnabled == c.IsEnabled)).Count();
        }

        public Task<Store> GetById(int id, bool includeDeleted = false)
        {
            return this.DbSet
                .Where(c => c.Id == id && (includeDeleted || !c.IsDeleted))
                .Include(c => c.Floor)
                .Include(c => c.Category)
                .Include(c => c.ShoppingCenter)
                .ThenInclude(c => c.Type)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
