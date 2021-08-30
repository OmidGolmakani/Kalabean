using Kalabean.Domain.Entities;
using Kalabean.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Mappers
{
    public interface ITicketDetailMapper
    {
        ThumbResponse<long> MapThumb(TicketDetail request);

    }
}
