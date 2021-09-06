using System;
using System.Collections.Generic;
using System.Text;

namespace Kalabean.Domain.Responses.Base
{
    interface IAuditResponse
    {
        string CreatedDate { get; set; }
        string CreatedBy { get; set; }
        string LastModified { get; set; }
        string LastModifiedBy { get; set; }
    }
}
