using System;

namespace Kalabean.Domain.Responses.Base
{
    public abstract class AuditResponse :  IAuditResponse
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
