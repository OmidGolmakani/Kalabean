using Kalabean.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public class ConversationDetail : AuditDeleteEntity
    {
        public long Id { get; set; }
        public int ConversationId { get; set; }
        public string Message { get; set; }
        public DateTime DateSeen { get; set; }
        public Conversation Conversation { get; set; }
    }
}
