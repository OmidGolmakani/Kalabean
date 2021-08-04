using System;
using System.Collections.Generic;
using System.Text;

namespace Kalabean.Domain.Responses
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public AccessRuleResponse AccessRule { get; set; }
        public byte? Order { get; set; }
        public CategoryResponse Parent { get; set; }
        public ICollection<CategoryResponse> Children { get; set; }
    }
}
