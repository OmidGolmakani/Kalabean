using Kalabean.Domain.Requests.City;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface ICityService
    {
        Task<IEnumerable<CityResponse>> GetCitiesAsync();
        Task<CityResponse> GetCityAsync(GetCityRequest request);
        Task<CityResponse> AddCityAsync(AddCityRequest request);
        Task<CityResponse> EditCityAsync(EditCityRequest request);
        Task BatchDeleteCitiesAsync(int[] Ids);
    }
}
