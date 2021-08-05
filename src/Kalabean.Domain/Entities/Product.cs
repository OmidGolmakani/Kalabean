using Kalabean.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public class Product: DeleteEntity
    {
        public long Id { get; set; }
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
        [ForeignKey("AccessRuleId")]
        public AccessRule AccessRule { get; set; }
        [ForeignKey("StoreId")]
        public Store Store { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
