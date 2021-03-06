using Kalabean.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public class Advertise : AuditDeleteEntity
    {
        public int Id { get; set; }
        public byte AdPositionId { get; set; }
        public string UrlLink { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool HasImage { get; set; }
    }
}
