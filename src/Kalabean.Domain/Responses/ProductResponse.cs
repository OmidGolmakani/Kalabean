using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class ProductResponse
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public int Num { get; set; }
        public byte? Order { get; set; }
        public string Manufacturer { get; set; }
        public string CompanyName { get; set; }
        public string PublishingDate { get; set; }
        public string ArchivingDate { get; set; }
        public string Model { get; set; }
        public string Series { get; set; }
        public string LinkProduct { get; set; }
        public string Keywords { get; set; }
        public string Properties { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public bool IsNew { get; set; }
        public bool IsEnabled { get; set; }
        public string FileUrl { get; set; }
        public TargetTypeResponse TargetType { get; set; }
        public ThumbResponse<long> UserThumb { get; set; }
        public List<ProductImageResponse> Images { get; set; }
        public ThumbResponse<int> StoreThumb { get; set; }
        public ThumbResponse<int> CategoryThumb { get; set; }

    }
}
