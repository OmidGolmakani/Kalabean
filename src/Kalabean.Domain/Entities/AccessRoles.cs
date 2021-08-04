using Kalabean.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kalabean.Domain.Entities
{
    public class AccessRule
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Category> Category { get; set; }
        public ICollection<City> City { get; set; }
        public ICollection<ShoppingCenterType> ShoppingCenterTypes { get; set; }
    }
}
