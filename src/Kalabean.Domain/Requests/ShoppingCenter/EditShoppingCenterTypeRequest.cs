using Microsoft.AspNetCore.Http;
using System;

namespace Kalabean.Domain.Requests.ShoppingCenter
{
    public class EditShoppingCenterTypeRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte? Order { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public IFormFile Image { get; set; }
        public bool ImageEdited { get; set; }
        public Guid? AccessRuleId { get; set; }
    }
}
