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
        private readonly AppDbContext _dbContext;
        private readonly SignInManager<User> _signInManager;

        public UserService(IUserRepository UserRepository,
                           IUserMapper UserMapper,
                           UserManager<User> userManager,
                           IUnitOfWork unitOfWork,
                           AppDbContext dbContext,
                           SignInManager<User> signInManager)
        {
            _UserRepository = UserRepository;
            _UserMapper = UserMapper;
            this._userManager = userManager;
            _unitOfWork = unitOfWork;
            this._dbContext = dbContext;
            this._signInManager = signInManager;
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
            item.PhoneNumberConfirmed = true;
            item.EmailConfirmed = true;
            var Result = await _userManager.CreateAsync(item, request.Password);
            if (Result.Succeeded)
            {
                return _UserMapper.Map(item);
            }
            else
            {
                return null;
            }
        }

        public Task<SigninResponse> SignIn(LoginRequest request)
        {
            var _user = _userManager.FindByNameAsync(request.UserName);
            Tuple<string, double> tokenInfo = null;
            SigninResponse Result = null;
            _user.Wait();
            if (_user.Result == null || _user.Result.Id == 0)
            {
                return Task.FromResult(new SigninResponse() { SignIn = SignInResult.Failed });
            }
            else
            {
                var SigninResult = _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, true);
                SigninResult.Wait();
                if (SigninResult.Result.Succeeded)
                {
                    tokenInfo = Helpers.JWTTokenManager.GenerateToken(request.UserName, _dbContext);
                    Result = new SigninResponse()
                    {
                        SignIn = SigninResult.Result,
                        UserId = _user.Result.Id,
                        Token = tokenInfo.Item1,
                        IsAdmin = _userManager.GetRolesAsync(_user.Result).Result.Count(x => x == "Administrator") == 0 ? false : true,
                    };
                }
                else
                {
                    Result = new SigninResponse()
                    {
                        UserId = _user.Result.Id,
                        SignIn = SigninResult.Result
                    };
                }
            }
            return Task.FromResult(Result);
        }

        public Task SignOut()
        {
            var Result = _signInManager.SignOutAsync();
            return Result;
        }
    }
}
