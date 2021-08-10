using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.RolePermission;
using Kalabean.Domain.Responses;
using System.Collections.Generic;

namespace Kalabean.Domain.Mappers
{
    public class RolePermissionMapper : IRolePermissionMapper
    {

        public RolePermissionMapper()
        {

        }

        public RolePermission Map(AddRolePermissionRequest request)
        {
            if (request == null) return null;

            var RolePermission = new RolePermission
            {
                Id = 0,
                IsDeleted = false,
                RoleId = request.RoleId,
                Token = request.Token,
                Url = request.Url
            };
            return RolePermission;
        }

        public RolePermission Map(EditRolePermissionRequest request)
        {
            if (request == null) return null;

            var RolePermission = new RolePermission
            {
                Id = request.Id,
                IsDeleted = false,
                RoleId = request.RoleId,
                Token = request.Token,
                Url = request.Url
            };

            return RolePermission;
        }
        public RolePermissionResponse Map(RolePermission RolePermission)
        {
            if (RolePermission == null) return null;

            var response = new RolePermissionResponse
            {
                Id = RolePermission.Id,
                RoleId = RolePermission.RoleId,
                Token = RolePermission.Token,
                Url = RolePermission.Url
            };

            return response;
        }
    }
}
