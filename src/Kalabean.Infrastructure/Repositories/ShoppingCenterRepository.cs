using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.ShoppingCenter;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
    public class ShoppingCenterRepository : Repository<ShoppingCenter>, IShoppingCenterRepository
    {
        //private readonly DbFactory _dbFactory;
        public ShoppingCenterRepository(DbFactory dbFactory) : base(dbFactory) { }

        public Task<ShoppingCenter> GetById(int id, bool includeDeleted = false)
        {
            return this.DbSet
                .Where(c => c.Id == id && (includeDeleted || !c.IsDeleted))
                .Include(c => c.Stores)
                .ThenInclude(c => c.Category)
                .Include(c => c.Type)
                .Include(c => c.City)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IQueryable<ShoppingCenter>> Get(GetShopingCentersRequest request, bool includeDeleted = false)
        {
            return this
                .List(c => (includeDeleted || !c.IsDeleted) &&
                (string.IsNullOrEmpty(request.Name) || 
                (!string.IsNullOrEmpty(c.Name) && c.Name.Contains(request.Name))) &&
                (!request.CityId.HasValue || c.CityId == request.CityId) &&
                (!request.TypeId.HasValue || c.TypeId == request.TypeId) &&
                (!request.IsEnabled.HasValue || c.IsEnabled == request.IsEnabled))
                 .Skip(request.PageSize * request.PageIndex).Take(request.PageSize)
                .Include(c => c.Type)
                .Include(c => c.City);
        }

        public async Task<int> Count(GetShopingCentersRequest request, bool includeDeleted = false)
        {
            return this
                .List(c => (includeDeleted || !c.IsDeleted) &&
                (string.IsNullOrEmpty(request.Name) ||
                (!string.IsNullOrEmpty(c.Name) && c.Name.Contains(request.Name))) &&
                (!request.CityId.HasValue || c.CityId == request.CityId) &&
                (!request.TypeId.HasValue || c.TypeId == request.TypeId) &&
                (!request.IsEnabled.HasValue || c.IsEnabled == request.IsEnabled)).Count();
        }
    }
}
