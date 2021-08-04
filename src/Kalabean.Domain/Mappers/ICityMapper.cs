using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.City;
using Kalabean.Domain.Responses;

namespace Kalabean.Domain.Mappers
{
    public interface ICityMapper
    {
        City Map(AddCityRequest request);
        City Map(EditCityRequest request);
        CityResponse Map(City request);

    }
}
