using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Requirement;
using Kalabean.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Mappers
{
    public class RequirementMapper : IRequirementMapper
    {
        private readonly ICategoryMapper _category;
        private readonly IProductMapper _product;

        public RequirementMapper(ICategoryMapper category,
                                 IProductMapper product)
        {
            this._category = category;
            this._product = product;
        }

        public Requirement Map(AddRequirementRequest request)
        {
            if (request == null) return null;
            var response = new Requirement()
            {
                CategoryId = request.CategoryId,
                Description = request.Description,
                Id = 0,
                IsDeleted = false,
                Price = request.Price,
                ProductId = request.ProductId,
                RequirementStatus = request.RequirementStatus,
                TypePricing = request.TypePricing,
                UserId = request.UserId,
                CreatedDate = DateTime.Now,
                HasImage = request.Image != null && request.Image.Length > 0,
            };
            return response;
        }

        public Requirement Map(EditRequirementRequest request)
        {
            if (request == null) return null;
            return new Requirement()
            {
                CategoryId = request.CategoryId,
                Description = request.Description,
                Id = request.Id,
                IsDeleted = false,
                Price = request.Price,
                ProductId = request.ProductId,
                RequirementStatus = request.RequirementStatus,
                TypePricing = request.TypePricing,
                UserId = request.UserId,
                HasImage = request.ImageEdited && request.Image != null && request.Image.Length > 0
            };
        }

        public RequirementResponse Map(Requirement request)
        {
            if (request == null) return null;

            return new RequirementResponse()
            {
                CategoryId = request.CategoryId,
                Description = request.Description,
                Id = request.Id,
                Price = request.Price,
                ProductId = request.ProductId,
                RequirementStatus = request.RequirementStatus,
                TypePricing = request.TypePricing,
                UserId = request.UserId,
                CategoryThumb = _category.MapThumb(request.Category),
                ProductThumb = _product.MapThumb(request.Product),
                ImageUrl = request.HasImage ? $"/KL_ImagesRepo/Requirement/250_250/{request.Id}.jpeg" : ""
            };
        }
    }
}
