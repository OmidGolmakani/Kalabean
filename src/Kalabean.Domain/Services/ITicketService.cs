using Kalabean.Domain.Requests.Ticket;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface ITicketService
    {
        Task<ListPagingResponse<TicketResponse>> GetTicketsAsync(GetTicketsRequest request);
        Task<TicketResponse> GetTicketAsync(GetTicketRequest request);
        Task<TicketResponse> AddTicketAsync(AddTicketRequest request);
    }
}
