using System;
using System.Collections.Generic;
using System.Text;

namespace Kalabean.Domain.Responses
{
    public class ShoppingCenterResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ShoppingCenterTypeResponse Type { get; set; }
        public CityResponse City { get; set; }
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
        public string ImageUrl { get; set; }
    }
}
