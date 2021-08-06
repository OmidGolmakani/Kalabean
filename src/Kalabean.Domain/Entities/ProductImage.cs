using Kalabean.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public class ProductImage : DeleteEntity
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Extention { get; set; }
        public Product Product { get; set; }
    }
}
