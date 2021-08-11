using System.Linq;
using System.Threading.Tasks;
using Kalabean.Domain.Entities;

namespace Kalabean.Domain.Repositories
{
    public interface IRolePermissionRepository : IRepository<RolePermission>
    {
        Task<RolePermission> GetById(long id);
        Task<IQueryable<RolePermission>> Get(long RoleId);
    }
}
