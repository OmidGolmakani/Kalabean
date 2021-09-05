using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public enum AdPositions : byte
    {
        [Display(Name ="صفحه اصلی - راست بالا")]
        TopRightHomePage = 1,
        [Display(Name = "صفحه اصلی - اسلاید چپ")]
        TopLeftHomePage = 2,
        [Display(Name = "صفحه اصلی - پایین")]
        BottomBelowHomePage = 3
    }
}
