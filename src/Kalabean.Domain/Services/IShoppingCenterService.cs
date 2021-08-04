using Kalabean.Domain.Requests.ShoppingCenter;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface IShoppingCenterService
    {
        Task<IEnumerable<ShoppingCenterResponse>> GetShoppingCentersAsync();
        Task<ShoppingCenterResponse> GetShoppingCenterAsync(GetShoppingCenterRequest request);
        Task<ShoppingCenterResponse> AddShoppingCenterAsync(AddShoppingCenterRequest request);
        Task<ShoppingCenterResponse> EditShoppingCenterAsync(EditShoppingCenterRequest request);
        Task BatchDeleteShoppingCentersAsync(int[] Ids);
    }
}
