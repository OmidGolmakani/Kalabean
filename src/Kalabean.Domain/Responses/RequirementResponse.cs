using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class RequirementResponse
    {
        public long Id { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public long UserId { get; set; }
        public byte RequirementStatus { get; set; }
        public byte TypePricing { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool HasImage { get; set; }
        public long? AdminId { get; set; }
        public DateTime? AdminDate { get; set; }
        public ThumbResponse<long> ProductThumb { get; set; }
        public ThumbResponse<int> CategoryThumb { get; set; }
    }
}
