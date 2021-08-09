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
                HasImage = request.HasImage,
                Id = 0,
                IsDeleted = false,
                OrderStatus = request.OrderStatus,
                PaymenyLink = request.PaymenyLink,
                StoreId = request.StoreId,
                UserId = request.UserId
            };
        }

        public OrderHeader Map(EditOrderHeaderRequest request)
        {
            if (request == null) return null;
            return new OrderHeader()
            {
                Description = request.Description,
                HasImage = request.HasImage,
                Id = request.Id,
                IsDeleted = false,
                OrderStatus = request.OrderStatus,
                PaymenyLink = request.PaymenyLink,
                StoreId = request.StoreId,
                UserId = request.UserId
            };
        }

        public OrderHeaderResponse Map(OrderHeader request)
        {
            if (request == null) return null;
            return new OrderHeaderResponse()
            {
                Description = request.Description,
                HasImage = request.HasImage,
                Id = request.Id,
                OrderStatus = request.OrderStatus,
                PaymenyLink = request.PaymenyLink,
                StoreId = request.StoreId,
                UserId = request.UserId,
                StoreThumb = _store.MapThumb(request.Store)
            };
        }
    }
}
