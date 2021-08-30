using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Repositories
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Task<Ticket> GetById(GetTicketRequest request, bool includeDeleted = false);
        Task<IQueryable<Ticket>> Get(GetTicketsRequest request, bool includeDeleted = false);
        Task<int> Count(GetTicketsRequest request, bool includeDeleted = false);
    }
}
