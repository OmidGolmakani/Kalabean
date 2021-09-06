using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class ConversationDetailResponse
    {
        public ThumbResponse<long> Sender { get; set; }
        public ThumbResponse<long> Receiver { get; set; }
        public string Body { get; set; }
        public string SendDate { get; set; }
    }
}
