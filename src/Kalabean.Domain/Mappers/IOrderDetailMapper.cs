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
    public interface IOrderDetailMapper
    {
        OrderDetail Map(AddOrderDetailRequest request);
        OrderHeader Map(EditOrderDetailRequest request);
        OrderDetailResponse Map(OrderDetail request);
    }
}
