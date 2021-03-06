using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class OrderDetailResponse
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public int Num { get; set; }
        public int Price { get; set; }
        public ThumbResponse<long> ProductThumb { get; set; }
        public ThumbResponse<long> ThumbOrder { get; set; }
    }
}
