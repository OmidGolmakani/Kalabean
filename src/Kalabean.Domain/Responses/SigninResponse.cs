using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class SigninResponse
    {
        public long UserId { get; set; }
        public SignInResult SignIn { get; set; }
        public double ExprireDate { get; set; }
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
    }
}
