using Kalabean.Domain.Entities.Base;
using System.Collections.Generic;

namespace Kalabean.Domain.Entities
{
    public class ShoppingCenter: AuditDeleteEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public ShoppingCenterType Type { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string WorkingHours { get; set; }
        public string Address { get; set; }
        public bool HasAuction { get; set; }
        public string Description { get; set; }
        public string ShoppingCenterServices { get; set; }
        public bool HasImage { get; set; }
        public string VirtualTourUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string TelegramUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public bool IsEnabled { get; set; }
        public ICollection<Floor> Floors { get; set; }
        public ICollection<Store> Stores { get; set; }
    }
}
