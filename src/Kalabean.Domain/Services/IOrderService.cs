using Kalabean.Domain.Requests.OrderHeader;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderHeaderResponse>> GetOrdersAsync(GetOrdersRequest request);
        Task<OrderHeaderResponse> GetOrderAsync(GetOrderHeaderRequest request);
        Task<OrderHeaderResponse> AddOrderAsync(AddOrderHeaderRequest request);
        Task<OrderHeaderResponse> EditOrderAsync(EditOrderHeaderRequest request);
        Task BatchDeleteOrdersAsync(long[] Ids);
        Task PublishOrderAsync(long Id);
        Task<long> GetOrderNumAsync();
    }
}
