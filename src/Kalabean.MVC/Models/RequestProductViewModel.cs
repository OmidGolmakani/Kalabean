using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.MVC.Models
{
    public class RequestProductViewModel
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public int CityId { get; set; }
        public Kalabean.Domain.Entities.RequirementTypePriceing TypePricing { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public bool ImageEdited { get; set; }
    }
}
