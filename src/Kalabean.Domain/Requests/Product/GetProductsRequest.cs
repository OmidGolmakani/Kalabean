using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.Product
{
    public class GetProductsRequest:Page.PageRequest
    {
        public string ProductName { get; set; }
        public int? StoreId { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsNew { get; set; }
        public bool? Publish { get; set; }
    }
}
