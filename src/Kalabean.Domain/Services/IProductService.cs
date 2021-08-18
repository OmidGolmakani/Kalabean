using Kalabean.Domain.Requests.City;
using Kalabean.Domain.Requests.Product;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface IProductService
    {
        Task<ListPagingResponse<ProductResponse>> GetProductsAsync(GetProductsRequest request);
        Task<ProductResponse> GetProductAsync(GetProductRequest request);
        Task<ProductResponse> AddProductAsync(AddProductRequest request);
        Task<ProductResponse> EditProductAsync(EditProductRequest request);
        Task BatchDeleteProductsAsync(long[] Ids);
    }
}
