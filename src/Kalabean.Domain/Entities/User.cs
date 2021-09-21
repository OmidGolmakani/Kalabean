using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public class User : IdentityUser<long>
    {
        public User()
        {
        }
        public string Name { get; set; }
        public string Family { get; set; }
        public override string UserName { get => base.UserName; set => base.UserName = value; }
        public override string NormalizedUserName { get => base.NormalizedUserName; set => base.NormalizedUserName = value; }
        public override string PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
        public override string Email { get => base.Email; set => base.Email = value; }
        public override string NormalizedEmail { get => base.NormalizedEmail; set => base.NormalizedEmail = value; }
        public override string SecurityStamp { get => base.SecurityStamp; set => base.SecurityStamp = value; }
        public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }
        public byte? UserStatus { get; set; }
        public string NationalCode { get; set; }
        public string IdCardNo { get; set; }
        public byte? Subscriptiontype { get; set; }
        public string Address { get; set; }
        public bool? HasImage { get; set; }
        public ICollection<Product> ProductUsers { get; set; }
        public ICollection<Requirement> RequirementUsers { get; set; }
        public ICollection<Requirement> RequirementAdmins { get; set; }
        public ICollection<OrderHeader> FromOrderHeaders { get; set; }
        public ICollection<OrderHeader> ToOrderHeaders { get; set; }
        public ICollection<Store> Stores { get; set; }
        public ICollection<Article> Articles { get; set; }
        public RequirementUserSeen RequiremenUserSeen { get; set; }
        public ICollection<ProductComment> ProductCommentUsers { get; set; }
        public ICollection<ProductComment> ProductCommentAdmins { get; set; }
        public ICollection<Conversation> SenderConversations { get; set; }
        public ICollection<Conversation> RecipientConversations { get; set; }
        public ICollection<ConversationDetail> ConversationDetails { get; set; }
        public ICollection<Ticket> SenderTickets { get; set; }
        public ICollection<Ticket> RecipientTickets { get; set; }
        public ICollection<TicketDetail> TicketDetails { get; set; }
        public ICollection<Favorites> Favorites { get; set; }
    }
}
