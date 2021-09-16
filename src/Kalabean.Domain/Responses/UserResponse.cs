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
        public string UserFullName { get { return string.Format("{0} {1}", this.Name, this.Family).Trim(); } }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public string LockoutEnd { get; set; }
        private byte? _UserStatus = 0;
        public byte? UserStatus { get { return _UserStatus; } set { _UserStatus = value; } }
        public Entities.UserStatus UserStatusName
        {
            get
            {
                if (_UserStatus.HasValue == false) return Entities.UserStatus.AwaitingApproval;
                Entities.UserStatus status = (Entities.UserStatus)Enum.Parse(typeof(Entities.UserStatus), _UserStatus.Value.ToString());
                return status;
            }
        }
        public string NationalCode { get; set; }
        public string IdCardNo { get; set; }
        private byte? _Subscriptiontype = 0;
        public byte? Subscriptiontype { get { return _Subscriptiontype; } set { _Subscriptiontype = value; } }
        public Entities.Subscriptiontype SubscriptiontypeName
        {
            get
            {
                if (_Subscriptiontype.HasValue == false) return Entities.Subscriptiontype.Personal;
                Entities.Subscriptiontype type = (Entities.Subscriptiontype)Enum.Parse(typeof(Entities.Subscriptiontype), _Subscriptiontype.Value.ToString());
                return type;
            }
        }
        public string Address { get; set; }
        public ICollection<ThumbResponse<int>> StoresThumb { get; set; }
        public ICollection<OrderHeaderResponse> FromOrders { get; set; }
        public ICollection<OrderHeaderResponse> ToOrders { get; set; }
        public ICollection<RequirementResponse> Requirements { get; set; }
    }
}

