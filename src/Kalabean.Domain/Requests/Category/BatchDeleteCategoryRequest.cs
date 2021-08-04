using System;
using System.Collections.Generic;
using System.Text;

namespace Kalabean.Domain.Requests.Category
{
    public class BatchDeleteCategoryRequest
    {
        public int[] Ids { get; set; }
    }
}
