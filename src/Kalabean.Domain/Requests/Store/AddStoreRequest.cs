using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kalabean.Domain.Requests.Store
{
    public class AddStoreRequest
    {
        public string Name { get; set; }
        public int ShoppingCenterId { get; set; }
        public int CategoryId { get; set; }
        public int? FloorId { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public float DiscountPercentage { get; set; }
        public decimal DiscountCoupon { get; set; }
        public float AuctionPercentage { get; set; }
        public long UserId { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string WorkingHours { get; set; }
        public string StoreNo { get; set; }
        public string VirtualTourUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string TelegramUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsFeatured { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public IFormFile Image { get; set; }
    }
}
