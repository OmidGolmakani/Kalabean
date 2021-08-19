using Kalabean.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public class Product: AuditDeleteEntity
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public int Num { get; set; }
        public byte? Order { get; set; }
        public int StoreId { get; set; }
        public string Manufacturer { get; set; }
        public string CompanyName { get; set; }
        public DateTime? PublishingDate { get; set; }
        public DateTime? ArchivingDate { get; set; }
        public string Model { get; set; }
        public string Series { get; set; }
        public string LinkProduct { get; set; }
        public string Keywords { get; set; }
        public string Properties { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public bool HasFile { get; set; }
        public string FileExtention { get; set; }
        public bool IsNew { get; set; }
        public bool IsEnabled { get; set; }
        public Store Store { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<Requirement> Requirements { get; set; }
        public ICollection<ProductComment> ProductComments { get; set; }

    }
}
