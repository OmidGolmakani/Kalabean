using System.Linq;
using System.Threading.Tasks;
using Kalabean.Domain.Entities;

namespace Kalabean.Domain.Repositories
{
    public interface IStoreRepository : IRepository<Store>
    {
        Task<Store> GetById(int id, bool includeDeleted = false);
        Task<IQueryable<Store>> Get(bool includeDeleted = false);
    }
}
