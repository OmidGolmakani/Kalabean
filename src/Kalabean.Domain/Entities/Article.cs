using Kalabean.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public class Article : AuditDeleteEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string KeyWords { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public bool HasImage { get; set; }
        public bool HasFile { get; set; }
        public bool SuggestedContent { get; set; }
        public bool ShowInPortal { get; set; }
        public long AdminId { get; set; }
        public User AdminUser { get; set; }
    }
}
