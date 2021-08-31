using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.Ticket
{
    public class AddTicketRequest
    {
        public long? RecipientUserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
