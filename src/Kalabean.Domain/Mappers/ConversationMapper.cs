using Kalabean.Domain.Entities;
using Kalabean.Domain.Helper;
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
        private readonly IUserMapper _user;

        public ConversationMapper(IConversationDetailMapper conversationDetail,
                                  IUserMapper user)
        {
            this._conversationDetail = conversationDetail;
            this._user = user;
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
                RequirementId = request.RequirementId,
                Title = request.Title,
                FromUser = _user.MapThumb(request.SenderUser),
                ToUser = _user.MapThumb(request.RecipientUser),
                SentDate = request.CreatedDate.ToDate(),
                LastMessageDate = request.ConversationDetails != null ? request.ConversationDetails.Max(d => d.CreatedDate).ToDate() : null,
                MessageCount = request.ConversationDetails != null ? request.ConversationDetails.Count : 0
            };
            return response;
        }

        public ConversationDetailResponse MapDetail(ConversationDetail request)
        {
            if (request == null) return null;
            return new ConversationDetailResponse()
            {
                Sender = _user.MapThumb(request.SenderUser),
                Receiver = request.SenderUserId == request.SenderUserId ?
                                   _user.MapThumb(request.Conversation.RecipientUser) : _user.MapThumb(request.SenderUser),
                Body = request.Message,
                SendDate = request.CreatedDate.ToShamsi(false)
            };
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
