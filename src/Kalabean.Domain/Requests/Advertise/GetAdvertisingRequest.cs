﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.Advertise
{
    public class GetAdvertisingRequest:Page.PageRequest
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }

    }
}
