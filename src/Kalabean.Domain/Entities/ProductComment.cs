using Kalabean.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public class ProductComment : AuditDeleteEntity
    {
        public int Id { get; set; }
        public long ProductId { get; set; }
        public long? UserId { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public long? AdminId { get; set; }
        public DateTime? DatePublished { get; set; }
        public byte Status { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        public User AdminUser { get; set; }
    }
}
