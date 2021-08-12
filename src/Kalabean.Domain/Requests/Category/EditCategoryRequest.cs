using Microsoft.AspNetCore.Http;
using System;

namespace Kalabean.Domain.Requests.Category
{
    public class EditCategoryRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public byte? Order { get; set; }
        public int? ParentId { get; set; }
    }
}
