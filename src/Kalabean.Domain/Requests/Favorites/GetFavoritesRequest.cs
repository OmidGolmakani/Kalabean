using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.Favorites
{
    public class GetFavoritesRequest:Page.PageRequest
    {
        public byte? TypeId { get; set; }
        public long? UserId { get; set; }
    }
}
