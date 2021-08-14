using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class ArticleUserSeenResponse
    {
        public long UserId { get; set; }
        public long ArticleId { get; set; }
    }
}
