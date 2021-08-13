using System;
using System.Collections.Generic;
using System.Text;

namespace Kalabean.Domain.Responses.Base
{
    interface IAuditResponse
    {
        DateTime CreatedDate { get; set; }
        string CreatedBy { get; set; }
        DateTime? LastModified { get; set; }
        string LastModifiedBy { get; set; }
    }
}
