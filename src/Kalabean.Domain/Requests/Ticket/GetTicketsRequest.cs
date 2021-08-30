using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.Ticket
{
    public class GetTicketsRequest : Page.PageRequest
    {
        public string Title { get; set; }
        public long? UserId { get; set; }
        public string Message { get; set; }
    }
}
