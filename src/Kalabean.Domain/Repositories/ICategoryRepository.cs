using System.Linq;
using System.Threading.Tasks;
using Kalabean.Domain.Entities;

namespace Kalabean.Domain.Repositories
{
    public interface ICategoryRepository: IRepository<Category>
    {
        Task<Category> GetById(int id, bool includeDeleted = false);
        Task<IQueryable<Category>> Get(string name, int? parentId, bool includeDeleted = false);
    }
}
