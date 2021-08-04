using Microsoft.AspNetCore.Http;
using System;

namespace Kalabean.Domain.Requests.City
{
    public class AddCityRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Order { get; set; }
        public IFormFile Image{ get; set; }
        public Guid? AccessRuleId { get; set; }
    }
}
