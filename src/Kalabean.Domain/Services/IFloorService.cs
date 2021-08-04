using Kalabean.Domain.Requests.City;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface IFloorService
    {
        Task<IEnumerable<FloorResponse>> GetFloorsAsync();
    }
}
