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
                HasImage = request.Image == null ? false : true,
                Id = 0,
                IsDeleted = false,
                Price = request.Price,
                ProductId = request.ProductId,
                RequirementStatus = request.RequirementStatus,
                TypePricing = request.TypePricing,
                UserId = request.UserId
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
                HasImage = request.Image == null ? false : true,
                Id = request.Id,
                IsDeleted = false,
                Price = request.Price,
                ProductId = request.ProductId,
                RequirementStatus = request.RequirementStatus,
                TypePricing = request.TypePricing,
                UserId = request.UserId

            };
        }

        public RequirementResponse Map(Requirement request)
        {
            if (request == null) return null;

            return new RequirementResponse()
            {
                CategoryId = request.CategoryId,
                Description = request.Description,
                HasImage = request.HasImage,
                Id = 0,
                Price = request.Price,
                ProductId = request.ProductId,
                RequirementStatus = request.RequirementStatus,
                TypePricing = request.TypePricing,
                UserId = request.UserId,
                CategoryThumb = _category.MapThumb(request.Category),
                ProductThumb = _product.MapThumb(request.Product),
            };
        }
    }
}
