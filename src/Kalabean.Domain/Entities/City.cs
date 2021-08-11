using Kalabean.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kalabean.Domain.Entities
{
    public class City: AuditDeleteEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte? Order { get; set; }
        public string Description { get; set; }
        public bool HasImage { get; set; }
        public ICollection<ShoppingCenter> ShoppingCenters { get; set; }
    }
}
