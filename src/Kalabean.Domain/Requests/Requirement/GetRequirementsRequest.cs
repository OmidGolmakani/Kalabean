using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.Requirement
{
    public class GetRequirementsRequest : Page.PageRequest
    {
        public int? CategoryId { get; set; }
        public string ProductName { get; set; }
        public long? UserId { get; set; }
    }
}
