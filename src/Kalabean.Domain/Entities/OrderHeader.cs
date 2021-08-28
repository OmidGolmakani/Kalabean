using Kalabean.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public class OrderHeader : AuditDeleteEntity
    {
        public long Id { get; set; }
        public int StoreId { get; set; }
        public long OrderNum { get; set; }
        public DateTime? PaymenyDate { get; set; }
        public long UserId { get; set; }
        public int OrderPrice { get; set; }
        public string PaymenyLink { get; set; }
        public string Description { get; set; }
        public byte OrderStatus { get; set; }
        public bool HasImage { get; set; }
        public bool Published { get; set; }
        public Store Store { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public User OrderUser { get; set; }
    }
}
