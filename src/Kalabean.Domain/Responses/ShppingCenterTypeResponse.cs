using System;
using System.Collections.Generic;
using System.Text;

namespace Kalabean.Domain.Responses
{
    public class ShoppingCenterTypeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte? Order { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public AccessRuleResponse AccessRule { get; set; }
    }
}
