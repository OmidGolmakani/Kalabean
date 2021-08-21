using Kalabean.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public class PossibilitiesShopCenter : AuditDeleteEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasImage { get; set; }
    }
}
