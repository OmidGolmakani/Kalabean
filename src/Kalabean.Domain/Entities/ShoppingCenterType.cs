using Kalabean.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Kalabean.Domain.Entities
{
    public class ShoppingCenterType: AuditDeleteEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte? Order { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public bool HasImage { get; set; }
        public Guid? AccessRuleId { get; set; }
        public AccessRule AccessRule { get; set; }
        public ICollection<ShoppingCenter> ShoppingCenters { get; set; }
        public ICollection<Store> Stores { get; set; }
    }
}
