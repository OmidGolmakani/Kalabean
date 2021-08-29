using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.OrderHeader
{
    public class GetOrdersRequest:Page.PageRequest
    {
        public string Type { get; set; }
        public DateTime? OrderFrom { get; set; }
        public DateTime? OrderTo { get; set; }
        public DateTime? PaymentFrom { get; set; }
        public DateTime? PaymentTo { get; set; }
        public long? FromUserId { get; set; }
        public long? ToUserId { get; set; }
        public byte? OrderType { get; set; }
        public int? StoreId { get; set; }
        public int? OrderNum { get; set; }

    }
}
