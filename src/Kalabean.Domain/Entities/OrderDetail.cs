using Kalabean.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public class OrderDetail : DeleteEntity
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public int ProductId { get; set; }
        public int Num { get; set; }
        public int Price { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public Product Product { get; set; }
    }
}
