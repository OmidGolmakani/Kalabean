using Kalabean.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public class TicketDetail : AuditDeleteEntity
    {
        public long Id { get; set; }
        public int TicketId { get; set; }
        public long SenderUserId { get; set; }
        public string Message { get; set; }
        public DateTime DateSeen { get; set; }
        public Ticket Ticket { get; set; }
        public User SenderUser { get; set; }
    }
}
