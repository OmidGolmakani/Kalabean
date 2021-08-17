using Microsoft.AspNetCore.Http;
using System;

namespace Kalabean.Domain.Requests.ShoppingCenter
{
    public class AddShoppingCenterTypeRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public byte? Order { get; set; }
        public IFormFile Image{ get; set; }
    }
}
