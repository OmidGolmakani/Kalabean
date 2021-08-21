using System.Linq;
using System.Threading.Tasks;
using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.User;

namespace Kalabean.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetById(long id, bool includeDeleted = false);
        Task<IQueryable<User>> Get(GetUsersRequest request, bool includeDeleted = false);
        Task<int> Count(GetUsersRequest request, bool includeDeleted = false);
    }
}
