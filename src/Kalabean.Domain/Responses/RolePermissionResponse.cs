using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class RolePermissionResponse
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string Url { get; set; }
        public string Token { get; set; }
        public ThumbResponse<long> Role { get; set; }
    }
}
