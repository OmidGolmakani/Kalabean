using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Category;
using Kalabean.Domain.Responses;

namespace Kalabean.Domain.Mappers
{
    public interface ICategoryMapper
    {
        Category Map(AddCategoryRequest request);
        Category Map(EditCategoryRequest request);
        CategoryResponse Map(Category request);
        ThumbResponse<int> MapThumb(Category request);
    }
}
