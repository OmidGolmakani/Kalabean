using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.MVC.Models
{
    public class UserProfileViewModel
    {
        private readonly string _basePath;

        public UserProfileViewModel(string basePath)
        {
            _basePath = basePath;
        }
        public long Id { get; set; }
        public string ImageOriginalPath { get; set; }
        public string ImageListPat
        {
            get
            {
                return string.Format("{0}/Users/250_250/{1}.jpeg", this._basePath, this.Id);
            }
        }
        public string Name { get; set; }
        public string Family { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public byte? UserStatus { get; set; }
        public string Address { get; set; }
        public string NationalCode { get; set; }
        public string IdCard { get; set; }
        public byte? SubscriptionType { get; set; }

    }
}
