using Kalabean.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public class Conversation : AuditDeleteEntity
    {
        public int Id { get; set; }
        public long? RequirementId { get; set; }
        public long SenderUserId { get; set; }
        public long? RecipientUserId { get; set; }
        public string Title { get; set; }
        public byte Status { get; set; }
        public User SenderUser { get; set; }
        public User RecipientUser { get; set; }
        public ICollection<ConversationDetail> ConversationDetails { get; set; }
    }
}
