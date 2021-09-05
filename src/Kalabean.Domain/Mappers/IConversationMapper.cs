using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Conversation;
using Kalabean.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Mappers
{
    public interface IConversationMapper
    {
        Conversation Map(AddConversationRequest request);
        ConversationResponse Map(Conversation request);
        ThumbResponse<long> MapThumb(Conversation request);
        ConversationDetailResponse MapDetail(ConversationDetail request);
    }
}
