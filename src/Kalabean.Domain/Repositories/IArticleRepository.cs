using System.Linq;
using System.Threading.Tasks;
using Kalabean.Domain.Entities;

namespace Kalabean.Domain.Repositories
{
    public interface IArticleRepository: IRepository<Article>
    {
        Task<Article> GetById(long id, bool includeDeleted = false);
        Task<IQueryable<Article>> Get(bool includeDeleted = false);
    }
}
