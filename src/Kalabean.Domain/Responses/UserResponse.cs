using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class UserResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<ThumbResponse<int>> StoresThumb { get; set; }
        public ICollection<OrderHeaderResponse> Orders { get; set; }
        public ICollection<RequirementResponse> Requirements { get; set; }
    }
}

