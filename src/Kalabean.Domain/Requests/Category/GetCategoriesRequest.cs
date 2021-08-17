using System;
using System.Collections.Generic;
using System.Text;

namespace Kalabean.Domain.Requests.Category
{
    public class GetCategoriesRequest
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
