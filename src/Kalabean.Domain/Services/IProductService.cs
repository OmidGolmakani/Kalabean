using Kalabean.Domain.Requests.City;
using Kalabean.Domain.Requests.Product;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetProductsAsync();
        Task<ProductResponse> GetCityAsync(GetProductRequest request);
        Task<ProductResponse> AddCityAsync(AddProductRequest request);
        Task<ProductResponse> EditCityAsync(EditProductRequest request);
        Task BatchDeleteCitiesAsync(int[] Ids);
    }
}
