using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.OrderHeader;
using Kalabean.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Mappers
{
    public class OrderHeaderMapper : IOrderHeaderMapper
    {
        private readonly IStoreMapper _store;
        private readonly IOrderDetailMapper _orderDetail;

        public OrderHeaderMapper(IStoreMapper store,
                                 IOrderDetailMapper orderDetail)
        {
            this._store = store;
            this._orderDetail = orderDetail;
        }

        public OrderHeader Map(AddOrderHeaderRequest request)
        {
            if (request == null) return null;
            return new OrderHeader()
            {
                Description = request.Description,
                HasImage = request.Image != null && request.Image.Length > 0,
                Id = 0,
                IsDeleted = false,
                PaymenyLink = request.PaymenyLink,
                StoreId = request.StoreId,
                OrderPrice = request.OrderDetail != null ?
                             request.OrderDetail.Price *
                             request.OrderDetail.Num : 0,
                CreatedDate = DateTime.Now,
            };
        }

        public OrderHeader Map(EditOrderHeaderRequest request)
        {
            if (request == null) return null;
            return new OrderHeader()
            {
                Description = request.Description,
                Id = request.id,
                IsDeleted = false,
                PaymenyLink = request.PaymenyLink,
                StoreId = request.StoreId,
                HasImage = request.ImageEdited && request.Image != null && request.Image.Length > 0
            };
        }

        public OrderHeaderResponse Map(OrderHeader request)
        {
            if (request == null) return null;
            return new OrderHeaderResponse()
            {
                Description = request.Description,
                Id = request.Id,
                OrderStatus = request.OrderStatus,
                PaymenyLink = request.PaymenyLink,
                StoreId = request.StoreId,
                UserId = request.UserId,
                StoreThumb = _store.MapThumb(request.Store),
                OrderDetails = request.OrderDetails == null || request.OrderDetails.Count == 0  ? null : request.OrderDetails.Select(d => _orderDetail.Map(d)).ToList(),
                ImageUrl = request.HasImage ? $"/KL_ImagesRepo/Orders/250_250/{request.Id}.jpeg" : ""
            };
        }

        public ThumbResponse<long> MapThumb(OrderHeader request)
        {
            if (request == null) return null;
            return new ThumbResponse<long>()
            {
                Id = request.Id,
                Name = ""
            };
        }
    }
}
