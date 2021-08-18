﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.OrderHeader
{
    public class EditOrderHeaderRequest
    {
        public long id { get; set; }
        public int StoreId { get; set; }
        public string PaymenyLink { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public bool ImageEdited { get; set; }
        public OrderDetail.EditOrderDetailRequest OrderDetail { get; set; }
    }
}
