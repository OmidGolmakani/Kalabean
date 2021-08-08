using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.Requirement
{
    public class EditRequirementRequest
    {
        public long Id { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public long UserId { get; set; }
        public byte RequirementStatus { get; set; }
        public byte TypePricing { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
