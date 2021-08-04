using System;
using System.Threading.Tasks;
using Kalabean.Domain.Entities;

namespace Kalabean.Domain.Repositories
{
    public interface IShoppingCenterTypeRepository : IRepository<ShoppingCenterType>
    {
        Task<ShoppingCenterType> GetById (int id, bool includeDeleted = false);
    }
}
