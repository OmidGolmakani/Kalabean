using Kalabean.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public class Favorites : AuditDeleteEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public byte TypeId { get; set; }
        public long RelatedId { get; set; }
        public User User { get; set; }
    }
}
