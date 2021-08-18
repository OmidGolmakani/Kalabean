using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.OrderDetail;
using Kalabean.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Mappers
{
    public class OrderDetailMapper : IOrderDetailMapper
    {
        private readonly IProductMapper _product;

        public OrderDetailMapper(IProductMapper product)
        {
            this._product = product;
        }

        public OrderDetail Map(AddOrderDetailRequest request)
        {
            if (request == null) return null;
            return new OrderDetail()
            {
                Id = 0,
                IsDeleted = false,
                Num = request.Num,
                Price = request.Price,
                ProductId = request.ProductId
            };
        }

        public OrderDetail Map(EditOrderDetailRequest request)
        {
            if (request == null) return null;
            return new OrderDetail()
            {
                Id = (request.Id ?? 0),
                IsDeleted = false,
                Num = request.Num,
                Price = request.Price,
                ProductId = request.ProductId
            };
        }

        public OrderDetailResponse Map(OrderDetail request)
        {
            if (request == null) return null;
            return new OrderDetailResponse()
            {
                Id = request.Id,
                Num = request.Num,
                OrderId = request.OrderId,
                Price = request.Price,
                ProductId = request.ProductId,
                ThumbOrder = null,
                ProductThumb = _product.MapThumb(request.Product)
            };
        }
    }
}
