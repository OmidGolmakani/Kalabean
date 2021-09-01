using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class RequirementResponse : Base.AuditResponse
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public long UserId { get; set; }
        public byte RequirementStatus { get; set; }
        public byte TypePricing { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public long? AdminId { get; set; }
        public double Expire { get; set; }
        public DateTime? DateChangeStatus { get; set; }
        public ThumbResponse<int> CategoryThumb { get; set; }
    }
}
