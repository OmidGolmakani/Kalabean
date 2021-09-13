using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.User
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        private bool _UseApi = true;
        public bool UseApi { get { return _UseApi; } set { _UseApi = value; } }
    }
}
