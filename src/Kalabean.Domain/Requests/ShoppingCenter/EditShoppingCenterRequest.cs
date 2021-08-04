using Microsoft.AspNetCore.Http;
using System;

namespace Kalabean.Domain.Requests.ShoppingCenter
{
    public class EditShoppingCenterRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public int CityId { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string WorkingHours { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int[] Services { get; set; }
        public string VirtualTourUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string TelegramUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public bool IsEnabled { get; set; }
        public bool HasAuction { get; set; }
        public IFormFile Image { get; set; }
        public bool ImageEdited { get; set; }
    }
}
