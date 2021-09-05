using Kalabean.Domain.Requests.Conversation;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface IConversationService
    {
        Task<ListPagingResponse<ConversationResponse>> GetConversationsAsync(GetConversationsRequest request);
        Task<ConversationResponse> GetConversationAsync(GetConversationRequest request);
        Task<ConversationResponse> AddConversationAsync(AddConversationRequest request);
        Task<ListPagingResponse<ConversationDetailResponse>> GetConversationDetailAsync(GetConversationRequest request);
    }
}
