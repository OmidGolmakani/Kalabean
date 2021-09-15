using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.User
{
    public class EditUserRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public byte? UserStatus { get; set; }
        public string Address { get; set; }
        public string NationalCode { get; set; }
        public string IdCard { get; set; }
        public byte? SubscriptionType { get; set; }
        public bool ImageEdited { get; set; }
        public IFormFile Image { get; set; }
    }
}
