using Kalabean.Domain.Requests.Advertise;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface IAdvertiseService
    {
        Task<ListPagingResponse<AdvertiseResponse>> GetAdvertisingAsync(GetAdvertisingRequest request);
        Task<AdvertiseResponse> GetAdvertiseAsync(GetAdvertiseRequest request);
        Task<AdvertiseResponse> AddAdvertiseAsync(AddAdvertiseRequest request);
        Task<AdvertiseResponse> EditAdvertiseAsync(EditAdvertiseRequest request);
        Task BatchDeleteAdvertisingAsync(int[] ids);
        Task<long> Count(GetAdvertisingRequest request);
    }
}
