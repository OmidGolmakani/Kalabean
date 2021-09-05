using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class ConversationResponse
    {
        public int Id { get; set; }
        public long? RequirementId { get; set; }
        public ThumbResponse<long> FromUser { get; set; }
        public ThumbResponse<long> ToUser { get; set; }
        public string Title { get; set; }
        public string SentDate { get; set; }
        public int MessageCount { get; set; }
        public string LastMessageDate { get; set; }
    }
}
