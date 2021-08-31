using Kalabean.Domain.Entities;
using Kalabean.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Mappers
{
    public class ConversationDetailMapper : IConversationDetailMapper
    {
        public ThumbResponse<long> MapThumb(ConversationDetail request)
        {
            if (request == null) return null;
            return new ThumbResponse<long>()
            {
                Id = request.Id,
                Name = request.Message
            };
        }
    }
}
