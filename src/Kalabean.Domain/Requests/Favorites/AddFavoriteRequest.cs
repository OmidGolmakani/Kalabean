using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.Favorites
{
    public class AddFavoriteRequest
    {
        public byte TypeId { get; set; }
        public long RelatedId { get; set; }
    }
}
