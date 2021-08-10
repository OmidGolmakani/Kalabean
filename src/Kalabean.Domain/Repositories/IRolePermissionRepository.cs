using System.Linq;
using System.Threading.Tasks;
using Kalabean.Domain.Entities;

namespace Kalabean.Domain.Repositories
{
    public interface IStoreRepository : IRepository<RolePermission>
    {
        Task<RolePermission> GetById(long id, bool includeDeleted = false);
        Task<IQueryable<RolePermission>> Get(bool includeDeleted = false);
    }
}
