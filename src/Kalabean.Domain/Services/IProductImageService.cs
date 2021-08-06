using Kalabean.Domain.Requests.City;
using Kalabean.Domain.Requests.Product;
using Kalabean.Domain.Requests.ProductImage;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface IProductImageService
    {
        Task<IEnumerable<ProductImageResponse>> GetProductsAsync();
        Task<ProductImageResponse> GetCityAsync(GetProductImageRequest request);
        Task<ProductImageResponse> AddCityAsync(AddProductImageRequest request);
        Task<ProductImageResponse> EditCityAsync(EditProductImageRequest request);
        Task BatchDeleteCitiesAsync(int[] Ids);
    }
}
