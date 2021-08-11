using Kalabean.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Kalabean.Domain.Entities
{
    public class Category: AuditDeleteEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte? Order { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public int? ParentId { get; set; }
        public ICollection<Category> Children { get; set; }
        public Category Parent { get; set; }
        public ICollection<Store> Stores { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Requirement> Requirements { get; set; }
    }
}
