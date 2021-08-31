using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.Conversation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
    public class ConversationRepository : Repository<Conversation>, IConversationRepository
    {
        private readonly DbFactory _dbFactory;
        public ConversationRepository(DbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<int> Count(GetConversationsRequest request, bool includeDeleted = false)
        {
            var Count = this
                 .List(p => (includeDeleted || !p.IsDeleted) &&
                 (request.UserId == null || p.RecipientUserId == null ||
                 p.RecipientUserId == request.UserId) &&
                 (request.UserId == null || p.SenderUserId == request.UserId) &&
                 (string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(p.Title) ||
                 p.Title.Contains(request.Title)) &&
                 (string.IsNullOrEmpty(request.Message) || p.ConversationDetails.Count == 0 ||
                 p.ConversationDetails.Any(d => string.IsNullOrEmpty(d.Message) || d.Message.Contains(request.Message)
                 )))
                 .Skip(request.PageSize * request.PageIndex).Take(request.PageSize).Count();
            return Count;
        }

        public async Task<IQueryable<Conversation>> Get(GetConversationsRequest request, bool includeDeleted = false)
        {
            return this
                 .List(p => (includeDeleted || !p.IsDeleted) &&
                 (request.UserId == null || p.RecipientUserId == null ||
                 p.RecipientUserId == request.UserId) &&
                 (request.UserId == null || p.SenderUserId == request.UserId) &&
                 (string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(p.Title) ||
                 p.Title.Contains(request.Title)) &&
                 (string.IsNullOrEmpty(request.Message) || p.ConversationDetails.Count == 0 ||
                 p.ConversationDetails.Any(d => string.IsNullOrEmpty(d.Message) || d.Message.Contains(request.Message)
                 )))
                 .Skip(request.PageSize * request.PageIndex).Take(request.PageSize)
                 .Include(p => p.ConversationDetails)
                 .Include(p => p.Requirement);
        }

        public Task<Conversation> GetById(GetConversationRequest request, bool includeDeleted = false)
        {
            return this.DbSet
             .Where(p => p.Id == request.Id && (includeDeleted || !p.IsDeleted))
             .AsNoTracking()
             .Include(p=> p.ConversationDetails)
             .Include(p => p.Requirement)
             .FirstOrDefaultAsync();
        }
        public override Conversation Add(Conversation entity)
        {
            var current = this.DbSet.FirstOrDefault(c => c.IsDeleted == false &&
                                               c.RequirementId == entity.RequirementId &&
                                               c.SenderUserId == entity.SenderUserId &&
                                               c.RecipientUserId == entity.RecipientUserId);
            if (current == null)
            {
                return base.Add(entity);
            }
            else
            {
                current.ConversationDetails = entity.ConversationDetails;
                return base.Update(current);
            }
        }
    }
}
