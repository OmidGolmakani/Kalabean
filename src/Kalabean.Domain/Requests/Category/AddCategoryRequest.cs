using System;
using System.Collections.Generic;
using System.Text;

namespace Kalabean.Domain.Requests.Category
{
    public class AddCategoryRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public byte? Order { get; set; }
        public int? ParentId { get; set; }
    }
}
