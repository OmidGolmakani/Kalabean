using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.ProductImage;
using Kalabean.Domain.Responses;

namespace Kalabean.Domain.Mappers
{
    public interface IProductImageMapper
    {
        ProductImage Map(AddProductImageRequest request);
        ProductImage Map(EditProductImageRequest request);
        ProductImageResponse Map(ProductImage request);

    }
}
