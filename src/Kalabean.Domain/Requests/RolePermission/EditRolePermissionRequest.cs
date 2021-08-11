using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.RolePermission
{
    public class EditRolePermissionRequest
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string Url { get; set; }
    }
}
