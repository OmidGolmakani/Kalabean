using System.Linq;
using Kalabean.Domain.Mappers;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.Conversation;
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
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IConversationMapper _conversationMapper;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public ConversationService(IConversationRepository conversationRepository,
                                   IConversationMapper conversationMapper,
                                   UserManager<User> userManager,
                                   IUnitOfWork unitOfWork)
        {
            _conversationRepository = conversationRepository;
            _conversationMapper = conversationMapper;
            this._userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<ListPagingResponse<ConversationResponse>> GetConversationsAsync(GetConversationsRequest request)
        {
            var UserId = Helpers.JWTTokenManager.GetUserIdByToken();
            var user = _userManager.Users.FirstOrDefault(u => u.Id == UserId);
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.FirstOrDefault(u => u == "Administrator") == null)
            {
                request.UserId = UserId;
            }
            var result = await _conversationRepository.Get(request);
            var list = result.Select(p => _conversationMapper.Map(p));
            var count = await _conversationRepository.Count(request);
            return new ListPagingResponse<ConversationResponse>() { Items = list, Total = count };
        }
        public async Task<ConversationResponse> GetConversationAsync(GetConversationRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var Conversation = await _conversationRepository.GetById(request);
            return _conversationMapper.Map(Conversation);
        }
        public async Task<ConversationResponse> AddConversationAsync(AddConversationRequest request)
        {
            var item = _conversationMapper.Map(request);
            item.SenderUserId = Helpers.JWTTokenManager.GetUserIdByToken();
            item.Status = (byte)ConversationStatus.Active;
            foreach (var d in item.ConversationDetails)
            {
                d.SenderUserId = item.SenderUserId;
            }
            var result = _conversationRepository.Add(item);
            await _unitOfWork.CommitAsync();
            return _conversationMapper.Map(await _conversationRepository.GetById(new GetConversationRequest() { Id = result.Id }));
        }
    }
}
