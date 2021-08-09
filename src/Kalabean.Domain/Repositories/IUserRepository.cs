using System.Linq;
using System.Threading.Tasks;
using Kalabean.Domain.Entities;

namespace Kalabean.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetById(long id);
        Task<IQueryable<User>> Get();
    }
}
