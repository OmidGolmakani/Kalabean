using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.Requirement
{
    public class AddRequirementRequest
    {
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public Entities.RequirementTypePriceing TypePricing { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
