using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.Ticket;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        private readonly DbFactory _dbFactory;
        public TicketRepository(DbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<int> Count(GetTicketsRequest request, bool includeDeleted = false)
        {
            var Count = this
                 .List(p => (includeDeleted || !p.IsDeleted) &&
                 (request.UserId == null || p.RecipientUserId == null ||
                 p.RecipientUserId == request.UserId) &&
                 (request.UserId == null || p.SenderUserId == request.UserId) &&
                 (string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(p.Title) ||
                 p.Title.Contains(request.Title)) &&
                 (string.IsNullOrEmpty(request.Message) || p.TicketDetails.Count == 0 ||
                 p.TicketDetails.Any(d => string.IsNullOrEmpty(d.Message) || d.Message.Contains(request.Message)
                 )))
                 .Skip(request.PageSize * request.PageIndex).Take(request.PageSize).Count();
            return Count;
        }

        public async Task<IQueryable<Ticket>> Get(GetTicketsRequest request, bool includeDeleted = false)
        {
            return this
                 .List(p => (includeDeleted || !p.IsDeleted) &&
                 (request.UserId == null || p.RecipientUserId == null ||
                 p.RecipientUserId == request.UserId) &&
                 (request.UserId == null || p.SenderUserId == request.UserId) &&
                 (string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(p.Title) ||
                 p.Title.Contains(request.Title)) &&
                 (string.IsNullOrEmpty(request.Message) || p.TicketDetails.Count == 0 ||
                 p.TicketDetails.Any(d => string.IsNullOrEmpty(d.Message) || d.Message.Contains(request.Message)
                 )))
                 .Skip(request.PageSize * request.PageIndex).Take(request.PageSize)
                 .Include(p => p.TicketDetails);
        }

        public Task<Ticket> GetById(GetTicketRequest request, bool includeDeleted = false)
        {
            return this.DbSet
             .Where(p => p.Id == request.Id && (includeDeleted || !p.IsDeleted))
             .AsNoTracking()
             .Include(p=> p.TicketDetails)
             .FirstOrDefaultAsync();
        }
        public override Ticket Add(Ticket entity)
        {
            var current = this.DbSet.FirstOrDefault(c => c.IsDeleted == false &&
                                                         c.SenderUserId == entity.SenderUserId &&
                                                         c.RecipientUserId == entity.RecipientUserId);
            if (current == null)
            {
                return base.Add(entity);
            }
            else
            {
                current.TicketDetails = entity.TicketDetails;
                return base.Update(current);
            }
        }
    }
}
