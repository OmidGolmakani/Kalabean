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
        public int CategoryId { get; set; }
        public int StoreId { get; set; }
        public byte? Order { get; set; }
        public string ProductName { get; set; }
        public int Num { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public string Creator { get; set; }
        public string DatePublish { get; set; }
        public string DateArchive { get; set; }
        public string Model { get; set; }
        public string Series { get; set; }
        public string LinkProduct { get; set; }
        public string Properties { get; set; }
        public string Description { get; set; }
        public bool IsNew { get; set; }
        public bool Publish { get; set; }
        public ThumbResponse<int> StoreThumb { get; set; }
        public ThumbResponse<int> CategoryThumb { get; set; }

    }
}
