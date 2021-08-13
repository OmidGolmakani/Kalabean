using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Article;
using Kalabean.Domain.Responses;

namespace Kalabean.Domain.Mappers
{
    public interface IArticleMapper
    {
        Article Map(AddArticleRequest request);
        Article Map(EditArticleRequest request);
        ArticleResponse Map(Article request);
        ThumbResponse<long> MapThumb(Article request);
    }
}
