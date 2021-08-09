using System.Linq;
using Kalabean.Domain.Mappers;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.User;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Kalabean.Domain.Base;
using Kalabean.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Kalabean.Domain.Entities;

namespace Kalabean.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;
        private readonly IUserMapper _UserMapper;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUserRepository UserRepository,
                           IUserMapper UserMapper,
                           UserManager<User> userManager,
                           IUnitOfWork unitOfWork)
        {
            _UserRepository = UserRepository;
            _UserMapper = UserMapper;
            this._userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserResponse>> GetUsersAsync()
        {
            var result = await _UserRepository.Get();
            return result.Select(c => _UserMapper.Map(c));
        }
        public async Task<UserResponse> GetUserAsync(GetUserRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var User = await _UserRepository.GetById(request.Id);
            return _UserMapper.Map(User);
        }
        public async Task<UserResponse> AddUserAsync(AddUserRequest request)
        {
            var item = _UserMapper.Map(request);
            var Result = await _userManager.CreateAsync(item);
            if (Result.Succeeded)
            {
                return _UserMapper.Map(item);
            }
            else
            {
                return null;
            }
        }
    }
}
