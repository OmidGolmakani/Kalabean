using System;
using System.Collections.Generic;
using System.Text;

namespace Kalabean.Domain.Responses
{
    public class FloorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte? Order { get; set; }
        public int ShoppingCenterId { get; set; }
    }
}
