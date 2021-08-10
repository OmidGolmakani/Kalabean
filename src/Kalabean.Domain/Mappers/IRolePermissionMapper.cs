using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Requirement;
using Kalabean.Domain.Requests.RolePermission;
using Kalabean.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Mappers
{
    public interface IRolePermissionMapper
    {
        RolePermission Map(AddRolePermissionRequest request);
        RolePermission Map(EditRolePermissionRequest request);
        RolePermissionResponse Map(RolePermission request);
    }
}
