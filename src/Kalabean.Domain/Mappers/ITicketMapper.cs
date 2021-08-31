using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Ticket;
using Kalabean.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Mappers
{
    public interface ITicketMapper
    {
        Ticket Map(AddTicketRequest request);
        TicketResponse Map(Ticket request);
        ThumbResponse<long> MapThumb(Ticket request);
    }
}
