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
                IsDeleted = false,
                ProductId = request.ProductId
            };
            return response;
        }

        public ProductImage Map(EditProductImageRequest request)
        {
            if (request == null) return null;
            var response = new ProductImage()
            {
                Id = request.Id,
                ProductId = request.ProductId,
                Extention = request.Extention
            };
            return response;
        }

        public ProductImageResponse Map(ProductImage image)
        {
            if (image == null) return null;
            var response = new ProductImageResponse
            {
                Id = image.Id,
                ImageUrl = $"/KL_ImagesRepo/Products/250_250/{image.Id}.jpeg"
            };
            return response;
        }
    }
}
