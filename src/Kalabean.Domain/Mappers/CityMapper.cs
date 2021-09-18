using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.City;
using Kalabean.Domain.Responses;

namespace Kalabean.Domain.Mappers
{
    public class CityMapper : ICityMapper
    {
        public CityMapper()
        {

        }

        public City Map(AddCityRequest request)
        {
            if (request == null) return null;

            var city = new City
            {
                Description = request.Description,
                Name = request.Name,
                Order = request.Order,
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
                Id = request.Id
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
                Name = city.Name,
                Order = city.Order,
                Description = city.Description,
                ImageUrl = null,
                ParentId = city.ParentId,
                State = city.State,
                ParentThumb = city.Parent == null ? null : MapThumb(city.Parent)
            };
            if (city.HasImage)
                response.ImageUrl = $"/KL_ImagesRepo/Cities/250_250/{city.Id}.jpeg";
            return response;
        }

        public ThumbResponse<int> MapThumb(City request)
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
