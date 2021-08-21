using System.Linq;
using System.Threading.Tasks;
using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Article;

namespace Kalabean.Domain.Repositories
{
    public interface IArticleRepository : IRepository<Article>
    {
        Task<Article> GetById(long id, bool includeDeleted = false);
        Task<IQueryable<Article>> Get(GetArticlesRequest request, bool includeDeleted = false);
        Task<int> Count(GetArticlesRequest request, bool includeDeleted = false);
    }
}
