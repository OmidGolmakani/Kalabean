using Kalabean.Domain.Requests.User;
using Kalabean.Domain.Responses;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> GetUsersAsync();
        Task<UserResponse> GetUserAsync(GetUserRequest request);
        Task<UserResponse> AddUserAsync(AddUserRequest request);
        Task<SigninResponse> SignIn(LoginRequest request);
        Task<List<IdentityResult>> AddUserToRole(AddUserToRoleRequest request);
        Task SignOut();
    }
}
