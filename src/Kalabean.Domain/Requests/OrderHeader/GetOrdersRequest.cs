using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.OrderHeader
{
    public class GetOrdersRequest : Page.PageRequest
    {
        public int? StoreId { get; set; }
        public long? ProductId { get; set; }
    }
}
