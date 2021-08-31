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
    public class TicketMapper : ITicketMapper
    {
        private readonly ITicketDetailMapper _TicketDetail;

        public TicketMapper(ITicketDetailMapper TicketDetail)
        {
            this._TicketDetail = TicketDetail;
        }

        public Ticket Map(AddTicketRequest request)
        {
            if (request == null) return null;
            Ticket response = new Ticket()
            {
                Title = request.Title,
                RecipientUserId = request.RecipientUserId,
                TicketDetails = new List<TicketDetail>(),
                CreatedDate = DateTime.Now
            };
            response.TicketDetails.Add(new TicketDetail()
            {
                CreatedDate = DateTime.Now,
                Message = request.Message,
                SenderUserId = response.SenderUserId
            });
            return response;
        }

        public TicketResponse Map(Ticket request)
        {
            if (request == null) return null;
            TicketResponse response = new TicketResponse()
            {
                Id = request.Id,
                RecipientUserId = request.RecipientUserId,
                SenderUserId = request.SenderUserId,
                Status = request.Status,
                Title = request.Title
            };
            if (request.TicketDetails != null && request.TicketDetails.Count != 0)
            {
                response.Messages = new List<ThumbResponse<long>>();
                response.Messages = request.TicketDetails.
                    Select(d => _TicketDetail.MapThumb(d)).ToList();
            }
            return response;
        }

        public ThumbResponse<long> MapThumb(Ticket request)
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
