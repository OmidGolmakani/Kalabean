using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public enum TicketStatus
    {
        [Display(Name ="فعال")]
        Active = 1,
        [Display(Name = "بسته شده")]
        Closed = 2
    }
}
