using Kalabean.Domain.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Kalabean.MVC.Models
{
    public class ProductViewModel
    {
        string _basePath;
        public ProductViewModel(string basePath) {
            _basePath = basePath;
        }
        public long Id { get; set; }
        public long? ImageId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public string FormattedPrice { get; set; }
        public string ImageListPath
        {
            get
            {
                if(this.ImageId.HasValue)
                    return string.Format("{0}/Products/245_205/{1}.jpeg", this._basePath, this.ImageId);
                return "";
            }
        }   
        public int? TargetId { get; set; }
        public string LinkProduct { get; set; }
        public string Description { get; set; }
        public List<ProductImageResponse> Images { get; set; } = new List<ProductImageResponse>();

    }
}
