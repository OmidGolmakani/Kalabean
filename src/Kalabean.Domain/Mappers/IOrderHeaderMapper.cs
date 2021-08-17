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
    public interface IOrderHeaderMapper
    {
        OrderHeader Map(AddOrderHeaderRequest request);
        OrderHeader Map(EditOrderHeaderRequest request);
        OrderHeaderResponse Map(OrderHeader request);
        ThumbResponse<long> MapThumb(OrderHeader request);
    }
}
