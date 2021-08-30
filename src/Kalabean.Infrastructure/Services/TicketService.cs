using System.Linq;
using Kalabean.Domain.Mappers;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.Ticket;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Kalabean.Domain.Base;
using Kalabean.Domain.Services;
using Kalabean.Domain.Entities;
using System.Drawing;
using Microsoft.Extensions.Options;
using Kalabean.Infrastructure.Services.Image;
using Kalabean.Infrastructure.Files;
using Kalabean.Infrastructure.AppSettingConfigs.Images;
using Kalabean.Domain.Requests.ResizeImage;
using Microsoft.AspNetCore.Identity;

namespace Kalabean.Infrastructure.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _TicketRepository;
        private readonly ITicketMapper _TicketMapper;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public TicketService(ITicketRepository TicketRepository,
                                   ITicketMapper TicketMapper,
                                   UserManager<User> userManager,
                                   IUnitOfWork unitOfWork)
        {
            _TicketRepository = TicketRepository;
            _TicketMapper = TicketMapper;
            this._userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<ListPagingResponse<TicketResponse>> GetTicketsAsync(GetTicketsRequest request)
        {
            var UserId = Helpers.JWTTokenManager.GetUserIdByToken();
            var user = _userManager.Users.FirstOrDefault(u => u.Id == UserId);
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.FirstOrDefault(u => u == "Administrator") == null)
            {
                request.UserId = UserId;
            }
            var result = await _TicketRepository.Get(request);
            var list = result.Select(p => _TicketMapper.Map(p));
            var count = await _TicketRepository.Count(request);
            return new ListPagingResponse<TicketResponse>() { Items = list, Total = count };
        }
        public async Task<TicketResponse> GetTicketAsync(GetTicketRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var Ticket = await _TicketRepository.GetById(request);
            return _TicketMapper.Map(Ticket);
        }
        public async Task<TicketResponse> AddTicketAsync(AddTicketRequest request)
        {
            var item = _TicketMapper.Map(request);
            item.SenderUserId = Helpers.JWTTokenManager.GetUserIdByToken();
            item.Status = (byte)TicketStatus.Active;
            foreach (var d in item.TicketDetails)
            {
                d.SenderUserId = item.SenderUserId;
            }
            var result = _TicketRepository.Add(item);
            await _unitOfWork.CommitAsync();
            return _TicketMapper.Map(await _TicketRepository.GetById(new GetTicketRequest() { Id = result.Id }));
        }
    }
}
