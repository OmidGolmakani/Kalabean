using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.OrderDetail
{
    public class AddOrderDetailRequest
    {
        public long OrderId { get; set; }
        public int ProductId { get; set; }
        public int Num { get; set; }
        public int Price { get; set; }
    }
}
