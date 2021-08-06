using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class ThumbResponse<T>
    {
        public T Id { get; set; }
        public string Name { get; set; }
    }
}
