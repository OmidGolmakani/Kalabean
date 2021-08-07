using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.OrderDetail
{
    public class EditOrderDetailRequest
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public int ProductId { get; set; }
        public int Num { get; set; }
        public int Price { get; set; }
    }
}
