using System;
using System.Collections.Generic;
using System.Text;

namespace Kalabean.Domain.Requests.Store
{
    public class BatchDeleteStoreRequest
    {
        public int[] Ids { get; set; }
    }
}
