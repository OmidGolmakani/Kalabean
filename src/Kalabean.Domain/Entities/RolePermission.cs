using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public class RolePermission
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string Url { get; set; }
        public string Token { get; set; }
        public Role Role { get; set; }
    }
}
