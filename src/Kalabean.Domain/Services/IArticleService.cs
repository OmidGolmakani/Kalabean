using Kalabean.Domain.Requests.Article;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleResponse>> GetArticlesAsync();
        Task<ArticleResponse> GetArticleAsync(GetArticleRequest request);
        Task<ArticleResponse> AddArticleAsync(AddArticleRequest request);
        Task<ArticleResponse> EditArticleAsync(EditArticleRequest request);
        Task BatchDeleteArticlesAsync(long[] ids);
    }
}
