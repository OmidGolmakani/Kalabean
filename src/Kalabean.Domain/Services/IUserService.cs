using Kalabean.Domain.Requests.User;
using Kalabean.Domain.Responses;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface IUserService
    {
        Task<ListPagingResponse<UserResponse>> GetUsersAsync(GetUsersRequest request);
        Task<long> Count(GetUsersRequest request);
        Task<UserResponse> GetUserAsync(GetUserRequest request);
        Task<UserResponse> AddUserAsync(AddUserRequest request);
        Task<UserResponse> EditUserAsync(EditUserRequest request);
        Task<SigninResponse> SignIn(LoginRequest request);
        Task<List<IdentityResult>> AddUserToRole(AddUserToRoleRequest request);
        Task<List<string>> GetUserRoles(GetUserRequest request);
        Task BatchDeleteUsersAsync(long[] Ids);
        Task SignOut();
        Task UserValidation(Entities.User user);
        Task<string> PhoneNumberConfirmation(string PhoneNumber);
        Task<IdentityResult> VerifyPhoneNumber(string PhoneNumber, string Token);
    }
}
