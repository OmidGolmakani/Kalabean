using Kalabean.Domain.Requests.RolePermission;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface IRolePermissionService
    {
        Task<IEnumerable<RolePermissionResponse>> GetRolePermissionsAsync();
        Task<RolePermissionResponse> GetRolePermissionAsync(GetRolePermissionRequest request);
        Task<RolePermissionResponse> AddRolePermissionAsync(AddRolePermissionRequest request);
        Task<RolePermissionResponse> EditRolePermissionAsync(EditRolePermissionRequest request);
        Task BatchDeleteRolePermissionsAsync(long[] Ids);
    }
}
