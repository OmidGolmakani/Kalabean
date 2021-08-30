using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class TicketResponse
    {
        public int Id { get; set; }
        public long SenderUserId { get; set; }
        public long? RecipientUserId { get; set; }
        public string Title { get; set; }
        public byte Status { get; set; }
        public ICollection<ThumbResponse<long>> Messages { get; set; }
    }
}
