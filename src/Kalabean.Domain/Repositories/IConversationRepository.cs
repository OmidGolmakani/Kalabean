using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Conversation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Repositories
{
    public interface IConversationRepository : IRepository<Conversation>
    {
        Task<Conversation> GetById(GetConversationRequest request, bool includeDeleted = false);
        Task<IQueryable<Conversation>> Get(GetConversationsRequest request, bool includeDeleted = false);
        Task<int> Count(GetConversationsRequest request, bool includeDeleted = false);
    }
}
