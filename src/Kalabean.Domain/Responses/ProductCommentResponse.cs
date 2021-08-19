using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class ProductCommentResponse
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long? UserId { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public long? AdminId { get; set; }
        public string DatePublished { get; set; }
        public byte Status { get; set; }
        public ThumbResponse<long> ProductThumb { get; set; }
        public ThumbResponse<long> UserThumb { get; set; }
        public ThumbResponse<long> AdminThumb { get; set; }
    }
}
