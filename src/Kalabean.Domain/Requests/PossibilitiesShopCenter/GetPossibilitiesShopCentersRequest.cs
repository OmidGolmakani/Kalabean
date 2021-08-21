using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.PossibilitiesShopCenter
{
    public class GetPossibilitiesShopCentersRequest:Page.PageRequest
    {
        public string Name { get; set; }
    }
}
