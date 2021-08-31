using System;
using System.Collections.Generic;
using System.Text;

namespace Kalabean.Domain.Responses
{
    public class StoreResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long UserId { get; set; }
        public int? TypeId { get; set; }
        public ThumbResponse<int> ShoppingCenter { get; set; }
        public ThumbResponse<int> Category { get; set; }
        public ThumbResponse<int> Floor { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public float DiscountPercentage { get; set; }
        public decimal DiscountCoupon { get; set; }
        public float AuctionPercentage { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string WorkingHours { get; set; }
        public string StoreNo { get; set; }
        public string VirtualTourUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string TelegramUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public string ImageUrl { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsFeatured { get; set; }
    }
}
