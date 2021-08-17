using Kalabean.Domain.Requests.Category;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetCategoriesAsync(GetCategoriesRequest request);
        Task<CategoryResponse> GetCategoryAsync(GetCategoryRequest request);
        Task<CategoryResponse> AddCategoryAsync(AddCategoryRequest request);
        Task<CategoryResponse> EditCategoryAsync(EditCategoryRequest request);
        Task BatchDeleteCategoriesAsync(int[] ids);
    }
}
