using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.Conversation
{
    public class AddConversationRequest
    {
        public long? RequirementId { get; set; }
        public long? RecipientUserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
