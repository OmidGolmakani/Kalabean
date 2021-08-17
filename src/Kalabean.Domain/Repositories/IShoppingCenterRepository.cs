using System;
using System.Linq;
using System.Threading.Tasks;
using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.ShoppingCenter;

namespace Kalabean.Domain.Repositories
{
    public interface IShoppingCenterRepository : IRepository<ShoppingCenter>
    {
        Task<ShoppingCenter> GetById (int id, bool includeDeleted = false);
        Task<IQueryable<ShoppingCenter>> Get(GetShopingCentersRequest request,bool includeDeleted = false);
        Task<long> Count(GetShopingCentersRequest request, bool includeDeleted = false);
    }
}
