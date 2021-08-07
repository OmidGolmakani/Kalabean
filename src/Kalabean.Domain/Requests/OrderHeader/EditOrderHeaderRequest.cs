using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.OrderHeader
{
    public class EditOrderHeaderRequest
    {
        public long Id { get; set; }
        public int StoreId { get; set; }
        public long UserId { get; set; }
        public int OrderPrice { get; set; }
        public string PaymenyLink { get; set; }
        public string Description { get; set; }
        public byte OrderStatus { get; set; }
        public bool HasImage { get; set; }
    }
}
