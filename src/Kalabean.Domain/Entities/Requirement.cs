﻿using Kalabean.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public class Requirement : DeleteEntity
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public int CategoryId { get; set; }
        public long UserId { get; set; }
        public byte RequirementStatus { get; set; }
        public byte TypePricing { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool HasImage { get; set; }
        public long? AdminId { get; set; }
        public DateTime? AdminDate { get; set; }
        public Product Product { get; set; }
        public Category Category { get; set; }

    }
}
