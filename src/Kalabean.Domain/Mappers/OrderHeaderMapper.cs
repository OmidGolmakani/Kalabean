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

        public OrderHeaderMapper(IStoreMapper store)
        {
            this._store = store;
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
                OrderStatus = request.OrderStatus,
                PaymenyLink = request.PaymenyLink,
                StoreId = request.StoreId,
                UserId = request.UserId,
                OrderPrice = request.OrderDetail != null ?
                             request.OrderDetail.Sum(x => x.Price) *
                             request.OrderDetail.Sum(x => x.Num) : 0,
                CreatedDate = DateTime.Now,
            };
        }

        public OrderHeader Map(EditOrderHeaderRequest request)
        {
            if (request == null) return null;
            return new OrderHeader()
            {
                Description = request.Description,
                Id = request.Id,
                IsDeleted = false,
                OrderStatus = request.OrderStatus,
                PaymenyLink = request.PaymenyLink,
                StoreId = request.StoreId,
                UserId = request.UserId,
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
                ImageUrl = request.HasImage ? $"/KL_ImagesRepo/Orders/250_250/{request.Id}.jpeg" : ""
            };
        }
    }
}
