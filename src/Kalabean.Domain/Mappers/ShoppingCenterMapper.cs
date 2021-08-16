using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.ShoppingCenter;
using Kalabean.Domain.Responses;
using System;
using System.Collections.Generic;

namespace Kalabean.Domain.Mappers
{
    public class ShoppingCenterMapper : IShoppingCenterMapper
    {
        private readonly ICityMapper _cityMapper;
        private readonly IShoppingCenterTypeMapper _typeMapper;
        private const char SERVICES_SEPARATOR = '|';
        public ShoppingCenterMapper(ICityMapper cityMapper,
            IShoppingCenterTypeMapper typeMapper)
        {
            _cityMapper = cityMapper;
            _typeMapper = typeMapper;
        }

        public ShoppingCenter Map(AddShoppingCenterRequest request)
        {
            if (request == null) return null;

            var shoppingCenter = new ShoppingCenter
            {
                Description = request.Description,
                Name = request.Name,
                HasImage = request.Image != null && request.Image.Length > 0,
                Address = request.Address,
                CityId = request.CityId,
                Email = request.Email,
                HasAuction = request.HasAuction,
                InstagramUrl = request.InstagramUrl,
                IsEnabled = request.IsEnabled,
                Tel = request.Tel,
                TypeId = request.TypeId,
                CreatedDate = DateTime.Now,
                WorkingHours = request.WorkingHours,
                TelegramUrl = request.TelegramUrl,
                VirtualTourUrl = request.VirtualTourUrl,
                WebsiteUrl = request.WebsiteUrl
            };

            if (request.Services != null && request.Services.Length > 0)
            {
                char separator = '|';
                shoppingCenter.ShoppingCenterServices = string.Join(separator, request.Services).Trim(separator);
            }

            return shoppingCenter;
        }

        public ShoppingCenter Map(EditShoppingCenterRequest request)
        {
            if (request == null) return null;

            var shoppingCenter = new ShoppingCenter
            {
                Id = request.Id,
                Description = request.Description,
                Name = request.Name,
                Address = request.Address,
                CityId = request.CityId,
                Email = request.Email,
                HasAuction = request.HasAuction,
                InstagramUrl = request.InstagramUrl,
                IsEnabled = request.IsEnabled,
                Tel = request.Tel,
                TypeId = request.TypeId,
                CreatedDate = DateTime.Now,
                WorkingHours = request.WorkingHours,
                TelegramUrl = request.TelegramUrl,
                VirtualTourUrl = request.VirtualTourUrl,
                WebsiteUrl = request.WebsiteUrl
            };

            if (request.Services != null && request.Services.Length > 0)
            {
                shoppingCenter.ShoppingCenterServices = string.Join(SERVICES_SEPARATOR, request.Services).Trim(SERVICES_SEPARATOR);
            }

            if (request.ImageEdited)
            {
                shoppingCenter.HasImage = request.Image != null && request.Image.Length > 0;
            }

            return shoppingCenter;
        }
        public ShoppingCenterResponse Map(ShoppingCenter shoppingCenter)
        {
            if (shoppingCenter == null) return null;

            var response = new ShoppingCenterResponse
            {
                Id = shoppingCenter.Id,
                Name = shoppingCenter.Name,
                Description = shoppingCenter.Description,
                ImageUrl = null,
                Address = shoppingCenter.Address,
                Email = shoppingCenter.Email,
                CityThumb = _cityMapper.MapThumb(shoppingCenter.City),
                HasAuction = shoppingCenter.HasAuction,
                InstagramUrl = shoppingCenter.InstagramUrl,
                IsEnabled = shoppingCenter.IsEnabled,
                Tel = shoppingCenter.Tel,
                TelegramUrl = shoppingCenter.TelegramUrl,
                VirtualTourUrl = shoppingCenter.VirtualTourUrl,
                WebsiteUrl = shoppingCenter.WebsiteUrl,
                WorkingHours = shoppingCenter.WorkingHours,
                TypeThumb = _typeMapper.MapThump(shoppingCenter.Type)
            };
            if (shoppingCenter.HasImage)
                response.ImageUrl = $"/KL_ImagesRepo/Sh_C/250_250/{shoppingCenter.Id}.jpeg";
            if (shoppingCenter.ShoppingCenterServices != null)
            {
                string[] services = shoppingCenter.ShoppingCenterServices.Split(SERVICES_SEPARATOR);
                List<int> serviceIds = new List<int>();
                foreach (string cell in services)
                    serviceIds.Add(int.Parse(cell));
                response.Services = serviceIds.ToArray();
            }
            return response;
        }

        public ThumbResponse<int> MapThumb(ShoppingCenter request)
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
