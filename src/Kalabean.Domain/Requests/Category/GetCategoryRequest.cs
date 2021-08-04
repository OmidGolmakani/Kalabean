using System;
using System.Collections.Generic;
using System.Text;

namespace Kalabean.Domain.Requests.Category
{
    public class GetCategoryRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public int? Order { get; set; }
        public int? ParentId { get; set; }
        public Guid AccessRuleId { get; set; }
    }
}
