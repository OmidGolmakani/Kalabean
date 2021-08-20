using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class ListPagingResponse<T>
    {
        public IEnumerable<T> Items { get; set; }
        public long Total { get; set; }
    }
}
