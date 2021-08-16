using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.ShoppingCenter;
using Kalabean.Domain.Responses;
using System;

namespace Kalabean.Domain.Mappers
{
    public class ShoppingCenterTypeMapper : IShoppingCenterTypeMapper
    {
        public ShoppingCenterTypeMapper()
        {
        }

        public ShoppingCenterType Map(AddShoppingCenterTypeRequest request)
        {
            if (request == null) return null;

            var type = new ShoppingCenterType
            {
                Description = request.Description,
                Name = request.Name,
                Order = request.Order,
                HasImage = request.Image != null && request.Image.Length > 0,
                HtmlContent = request.HtmlContent,
                CreatedDate = DateTime.Now
            };

            return type;
        }

        public ShoppingCenterType Map(EditShoppingCenterTypeRequest request)
        {
            if (request == null) return null;

            var type = new ShoppingCenterType
            {
                Description = request.Description,
                Name = request.Name,
                Order = request.Order,
                Id = request.Id,
                HtmlContent = request.HtmlContent,
                LastModified = DateTime.Now
            };

            if (request.ImageEdited)
            {
                type.HasImage = request.Image != null && request.Image.Length > 0;
            }

            return type;
        }
        public ShoppingCenterTypeResponse Map(ShoppingCenterType type)
        {
            if (type == null) return null;

            var response = new ShoppingCenterTypeResponse
            {
                Id = type.Id,
                Name = type.Name,
                Order = type.Order,
                Description = type.Description,
                ImageUrl = null
            };
            if (type.HasImage)
                response.ImageUrl = $"/KL_ImagesRepo/Sh_C_Types/250_250/{type.Id}.jpeg";
            return response;
        }

        public ThumbResponse<int> MapThump(ShoppingCenterType request)
        {
            if (request == null) return null;
            return new ThumbResponse<int>()
            {
                Id = request.Id,
                Name = request.Name
            };
        }
    }
}
