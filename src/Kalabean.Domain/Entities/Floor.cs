using Kalabean.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kalabean.Domain.Entities
{
    public class Floor: DeleteEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte? Order { get; set; }
        public int ShoppingCenterId { get; set; }
        public ShoppingCenter ShoppingCenter { get; set; }
        public ICollection<Store> Stores { get; set; }
    }
}
