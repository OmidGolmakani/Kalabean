using System.Linq;
using System.Threading.Tasks;
using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Store;

namespace Kalabean.Domain.Repositories
{
    public interface IStoreRepository : IRepository<Store>
    {
        Task<Store> GetById(int id, bool includeDeleted = false);
        Task<IQueryable<Store>> Get(GetStoresRequest request, bool includeDeleted = false);
        Task<int> Count(GetStoresRequest request, bool includeDeleted = false);
    }
}
