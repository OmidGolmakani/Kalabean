using Kalabean.Domain.Requests.OrderHeader;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderHeaderResponse>> GetOrdersAsync();
        Task<OrderHeaderResponse> GetOrderAsync(GetOrderHeaderRequest request);
        Task<OrderHeaderResponse> AddOrderAsync(AddOrderHeaderRequest request);
        Task<OrderHeaderResponse> EditOrderHeaderAsync(EditOrderHeaderRequest request);
        Task BatchDeleteOrderAsync(int[] Ids);
    }
}
