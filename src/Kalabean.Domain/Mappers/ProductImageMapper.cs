using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.ProductImage;
using Kalabean.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Mappers
{
    public class ProductImageMapper : IProductImageMapper
    {
        public ProductImage Map(AddProductImageRequest request)
        {
            if (request == null) return null;
            var response = new ProductImage()
            {
                Id = 0,
                IsDeleted = false,
                ProductId = request.ProductId,
                Extention = request.Extention
            };
            return response;
        }

        public ProductImage Map(EditProductImageRequest request)
        {
            if (request == null) return null;
            var response = new ProductImage()
            {
                Id = request.Id,
                IsDeleted = false,
                ProductId = request.ProductId,
                Extention = request.Extention
            };
            return response;
        }

        public ProductImageResponse Map(ProductImage request)
        {
            if (request == null) return null;
            var response = new ProductImageResponse()
            {
                Id = request.Id,
                Extention = request.Extention,
                ProductId = request.ProductId
                //ProductThumb = _product.MapThumb(request.Product)
            };
            return response;
        }
    }
}
