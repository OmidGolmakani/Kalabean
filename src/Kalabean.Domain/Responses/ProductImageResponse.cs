using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class ProductImageResponse
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Extention { get; set; }
        public ThumbResponse<long> ProductThumb { get; set; }
    }
}
