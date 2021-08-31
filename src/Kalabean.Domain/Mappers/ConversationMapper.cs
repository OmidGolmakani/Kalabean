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
    public class ConversationMapper : IConversationMapper
    {
        private readonly IConversationDetailMapper _conversationDetail;

        public ConversationMapper(IConversationDetailMapper conversationDetail)
        {
            this._conversationDetail = conversationDetail;
        }

        public Conversation Map(AddConversationRequest request)
        {
            if (request == null) return null;
            Conversation response = new Conversation()
            {
                RequirementId = request.RequirementId,
                Title = request.Title,
                RecipientUserId = request.RecipientUserId,
                ConversationDetails = new List<ConversationDetail>(),
                CreatedDate = DateTime.Now
            };
            response.ConversationDetails.Add(new ConversationDetail()
            {
                CreatedDate = DateTime.Now,
                Message = request.Message,
                SenderUserId = response.SenderUserId
            });
            return response;
        }

        public ConversationResponse Map(Conversation request)
        {
            if (request == null) return null;
            ConversationResponse response = new ConversationResponse()
            {
                Id = request.Id,
                RecipientUserId = request.RecipientUserId,
                RequirementId = request.RequirementId,
                SenderUserId = request.SenderUserId,
                Status = request.Status,
                Title = request.Title
            };
            if (request.ConversationDetails != null && request.ConversationDetails.Count != 0)
            {
                response.Messages = new List<ThumbResponse<long>>();
                response.Messages = request.ConversationDetails.
                    Select(d => _conversationDetail.MapThumb(d)).ToList();
            }
            return response;
        }

        public ThumbResponse<long> MapThumb(Conversation request)
        {
            if (request == null) return null;
            return new ThumbResponse<long>()
            {
                Id = request.Id,
                Name = request.Title
            };
        }
    }
}
