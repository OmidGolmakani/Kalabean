using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.Product
{
    public class AddProductRequest
    {
        public int CategoryId { get; set; }
        public int StoreId { get; set; }
        public byte? Order { get; set; }
        public string ProductName { get; set; }
        public int Num { get; set; }
        public decimal Price { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public string Creator { get; set; }
        public Guid? AccessRuleId { get; set; }
        public DateTime DatePublish { get; set; }
        public DateTime DateArchive { get; set; }
        public string Model { get; set; }
        public string Series { get; set; }
        public string LinkProduct { get; set; }
        public string Properties { get; set; }
        public string Description { get; set; }
        public bool IsNew { get; set; }
        public bool Publish { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
