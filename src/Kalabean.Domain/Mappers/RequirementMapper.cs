using Kalabean.Domain.Entities;
using Kalabean.Domain.Helper;
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
        private readonly ICityMapper _city;

        public RequirementMapper(ICategoryMapper category,
                                 IProductMapper product,
                                 ICityMapper city)
        {
            this._category = category;
            this._product = product;
            this._city = city;
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
                ProductName = request.ProductName,
                TypePricing = (byte)request.TypePricing,
                CreatedDate = DateTime.Now,
                CityId = request.CityId,
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
                ProductName = request.ProductName,
                TypePricing = (byte)request.TypePricing,
                CityId = request.CityId,
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
                ProductName = request.ProductName,
                RequirementStatus = request.RequirementStatus,
                TypePricing = request.TypePricing,
                UserId = request.UserId,
                CreatedBy = request.CreatedBy,
                AdminId = request.AdminId,
                CreatedDate = request.CreatedDate.ToDate(),
                DateChangeStatus = request.DateChangeStatus == null ? null : request.DateChangeStatus.ToDate(),
                Expire = request.Expire,
                ConversationId = request.Conversations == null || request.Conversations.Count == 0 ? (int?)null : request.Conversations.FirstOrDefault().Id,
                CategoryThumb = _category.MapThumb(request.Category),
                CityThumb = _city.MapThumb(request.City),
                CityId = request.CityId,
                ImageUrl = request.HasImage ? $"/KL_ImagesRepo/Requirement/250_250/{request.Id}.jpeg" : ""
            };
        }
    }
}
