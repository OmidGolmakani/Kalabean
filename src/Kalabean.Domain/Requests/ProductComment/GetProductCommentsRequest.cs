using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.ProductComment
{
    public class GetProductCommentsRequest : Page.PageRequest
    {
        public long? ProductId { get; set; }
        public long? UserId { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
