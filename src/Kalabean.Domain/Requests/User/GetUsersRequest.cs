using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.User
{
    public class GetUsersRequest:Page.PageRequest
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string PhoneNUmber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
