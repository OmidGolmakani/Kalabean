using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.City;
using Kalabean.Domain.Responses;

namespace Kalabean.Domain.Mappers
{
    public class CityMapper: ICityMapper
    {
        IAccessRuleMapper _accessRuleMapper;
        public CityMapper(IAccessRuleMapper accessRuleMapper)
        {
            _accessRuleMapper = accessRuleMapper;
        }

        public City Map(AddCityRequest request)
        {
            if (request == null) return null;

            var city = new City
            {
                Description  = request.Description,
                Name = request.Name,
                Order = request.Order,
                AccessRuleId = request.AccessRuleId,
                HasImage = request.Image != null && request.Image.Length > 0
            };

            return city;
        }

        public City Map(EditCityRequest request)
        {
            if (request == null) return null;

            var city = new City
            {
                Description = request.Description,
                Name = request.Name,
                Order = request.Order,
                Id = request.Id,
                AccessRuleId = request.AccessRuleId
            };

            if (request.ImageEdited)
            {
                city.HasImage = request.Image != null && request.Image.Length > 0;
            }

            return city;
        }
        public CityResponse Map(City city)
        {
            if (city == null) return null;

            var response = new CityResponse
            {
                Id = city.Id,
                Name  = city.Name,
                Order = city.Order,
                Description = city.Description,
                ImageUrl = null
            };
            if (city.HasImage)
                response.ImageUrl = $"/KL_ImagesRepo/Cities/{city.Id}.jpeg";
            if (city.AccessRuleId.HasValue)
            {
                response.AccessRule = _accessRuleMapper.Map(city.AccessRule);
            }
            return response;
        }
    }
}
