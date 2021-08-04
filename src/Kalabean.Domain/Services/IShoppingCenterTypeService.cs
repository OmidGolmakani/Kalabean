using Kalabean.Domain.Requests.ShoppingCenter;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface IShoppingCenterTypeService
    {
        Task<IEnumerable<ShoppingCenterTypeResponse>> GetTypesAsync();
        Task<ShoppingCenterTypeResponse> GetTypeAsync(GetShoppingCenterTypeRequest request);
        Task<ShoppingCenterTypeResponse> AddTypeAsync(AddShoppingCenterTypeRequest request);
        Task<ShoppingCenterTypeResponse> EditTypeAsync(EditShoppingCenterTypeRequest request);
        Task BatchDeleteTypesAsync(int[] Ids);
    }
}
