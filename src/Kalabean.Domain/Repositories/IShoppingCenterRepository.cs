using System;
using System.Linq;
using System.Threading.Tasks;
using Kalabean.Domain.Entities;

namespace Kalabean.Domain.Repositories
{
    public interface IShoppingCenterRepository : IRepository<ShoppingCenter>
    {
        Task<ShoppingCenter> GetById (int id, bool includeDeleted = false);
        Task<IQueryable<ShoppingCenter>> Get(bool includeDeleted = false);
    }
}
