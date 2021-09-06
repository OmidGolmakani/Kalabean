using System;

namespace Kalabean.Domain.Responses.Base
{
    public abstract class AuditResponse :  IAuditResponse
    {
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastModified { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
