using Microsoft.AspNetCore.Http;
using System;

namespace Kalabean.Domain.Requests.City
{
    public class EditCityRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Order { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public bool ImageEdited { get; set; }
        public Guid? AccessRuleId { get; set; }
    }
}
