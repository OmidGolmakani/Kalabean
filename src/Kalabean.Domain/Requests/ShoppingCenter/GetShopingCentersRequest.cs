using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.ShoppingCenter
{
    public class GetShopingCentersRequest : Page.PageRequest
    {
        public string Name { get; set; }
        public int? CityId { get; set; }
        public int? TypeId { get; set; }
        public bool? IsEnabled { get; set; }
    }
}
