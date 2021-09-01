using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.OrderHeader
{
    public class AddOrderHeaderRequest
    {
        public int StoreId { get; set; }
        public string PaymentLink { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Description { get; set; }
        public long ToUserId { get; set; }
        public IFormFile Image { get; set; }
        public OrderDetail.AddOrderDetailRequest OrderDetail { get; set; }
    }
}
