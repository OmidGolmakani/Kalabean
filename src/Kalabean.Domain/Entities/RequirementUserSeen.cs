using Kalabean.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public class RequirementUserSeen : DeleteEntity
    {
        public long Id { get; set; }
        public long RequirementId { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public Requirement Requirement { get; set; }
    }
}
