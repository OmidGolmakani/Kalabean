using Kalabean.Domain.Requests.Store;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreResponse>> GetStoresAsync();
        Task<StoreResponse> GetStoreAsync(GetStoreRequest request);
        Task<StoreResponse> AddStoreAsync(AddStoreRequest request);
        Task<StoreResponse> EditStoreAsync(EditStoreRequest request);
        Task BatchDeleteStoresAsync(int[] Ids);
    }
}
