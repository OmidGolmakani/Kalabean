﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.Page
{
    public class PageRequest
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
