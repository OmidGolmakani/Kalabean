using System;
using System.Collections.Generic;
using System.Text;

namespace Kalabean.Domain.Requests.Store
{
    public class GetStoresRequest: Page.PageRequest
    {
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsEnabled { get; set; }
    }
}
