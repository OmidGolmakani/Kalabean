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
using Kalabean.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;

namespace Kalabean.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserMapper _userMapper;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _dbContext;
        private readonly SignInManager<User> _signInManager;
        private readonly HttpContext _httpContext;

        public UserService(IUserRepository userRepository,
                           IUserMapper userMapper,
                           UserManager<User> userManager,
                           IUnitOfWork unitOfWork,
                           AppDbContext dbContext,
                           SignInManager<User> signInManager,
                           IHttpContextAccessor httpContext)
        {
            _userRepository = userRepository;
            _userMapper = userMapper;
            this._userManager = userManager;
            _unitOfWork = unitOfWork;
            this._dbContext = dbContext;
            this._signInManager = signInManager;
            this._httpContext = httpContext.HttpContext;
        }

        public async Task<ListPagingResponse<UserResponse>> GetUsersAsync(GetUsersRequest request)
        {
            var result = await _userRepository.Get(request);
            var list = result.Select(u => _userMapper.Map(u));
            var count = await _userRepository.Count(request);
            return new ListPagingResponse<UserResponse>() { Items = list, Total = count };
        }
        public async Task<UserResponse> GetUserAsync(GetUserRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var User = await _userRepository.GetById(request.Id);
            return _userMapper.Map(User);
        }
        public async Task<UserResponse> AddUserAsync(AddUserRequest request)
        {
            var item = _userMapper.Map(request);
            await UserValidation(item);
            item.PhoneNumberConfirmed = true;
            item.EmailConfirmed = true;
            var Result = await _userManager.CreateAsync(item, request.Password);
            if (Result.Succeeded)
            {
                return _userMapper.Map(item);
            }
            else
            {
                return null;
            }
        }

        public async Task<UserResponse> EditUserAsync(EditUserRequest request)
        {
            var existingRecord = _userManager.Users.FirstOrDefault(x => x.Id == request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");
            existingRecord.UserName = request.UserName;
            existingRecord.PhoneNumber = request.PhoneNumber;
            existingRecord.Email = request.Email;
            existingRecord.Name = request.Name;
            existingRecord.Family = request.Family;
            await UserValidation(existingRecord);
            var Result = await _userManager.UpdateAsync(existingRecord);
            if (Result.Succeeded)
            {
                return _userMapper.Map(existingRecord);
            }
            else
            {
                return null;
            }
        }

        public Task<SigninResponse> SignIn(LoginRequest request)
        {
            var _user = _userManager.FindByNameAsync(request.UserName ?? "");
            Tuple<string, double> tokenInfo = null;
            SigninResponse Result = null;
            _user.Wait();
            if (_user.Result == null || _user.Result.Id == 0)
            {
                return Task.FromResult(new SigninResponse() { SignIn = SignInResult.Failed });
            }
            else
            {
                var SigninResult = _signInManager.PasswordSignInAsync(request.UserName ?? "", request.Password ?? "", false, true);
                SigninResult.Wait();
                if (SigninResult.Result.Succeeded)
                {
                    tokenInfo = Helpers.JWTTokenManager.GenerateToken(request.UserName, _dbContext);
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.MaxValue;
                    _httpContext.Response.Cookies.Append("AccessToken", tokenInfo.Item1, options);
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
            _httpContext.Response.Cookies.Delete("AccessToken");
            return Result;
        }

        public Task<List<IdentityResult>> AddUserToRole(AddUserToRoleRequest request)
        {
            var _user = _userManager.FindByIdAsync(request.Id.ToString());
            List<IdentityResult> Result = new List<IdentityResult>();
            foreach (string Role in request.Roles)
            {
                var RoleTask = _userManager.AddToRoleAsync(_user.Result, Role);
                Result.Add(RoleTask.Result);
            }
            return Task.FromResult(Result);
        }

        public Task BatchDeleteUsersAsync(long[] Ids)
        {
            List<Domain.Entities.User> users =
              _userManager.Users.Where(u => Ids.Contains(u.Id)).ToList();
            foreach (Domain.Entities.User user in users)
            {
                _userManager.SetLockoutEnabledAsync(user, true);
                _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            }
            return Task.Run(() => true);
        }

        public async Task<long> Count(GetUsersRequest request)
        {
            if (request == null) throw new ArgumentNullException();
            var User = await _userRepository.Count(request);
            return User;
        }

        public async Task UserValidation(User user)
        {
            var users = await GetUsersAsync(new GetUsersRequest());
            var _users = users.Items.Where(x => x.Id != user.Id);
            ErrorResponseV2 Error = new ErrorResponseV2() { StatusCode = 400 };
            if (users.Total == 0) return;
            if (_users.Count(u => u.UserName == user.UserName) != 0)
            {
                Error.MsgErrors.Add("نام کاربری تکراری می باشد");
            }
            if (_users.Count(u => u.PhoneNumber == user.PhoneNumber) != 0)
            {
                Error.MsgErrors.Add("تلفن همراه تکراری می باشد");
            }
            if (string.IsNullOrEmpty(user.Email) == false && _users.Count(u => u.Email == user.Email) != 0)
            {
                Error.MsgErrors.Add("پست الکترونیک تکراری می باشد");
            }
            if (Error.MsgErrors.Count != 0) throw new Exception(Error.JsonConvert());
        }
    }
}
