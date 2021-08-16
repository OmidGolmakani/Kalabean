using Kalabean.Domain.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class ArticleResponse : AuditResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string KeyWords { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string FileUrl { get; set; }
        public string FileExtention { get; set; }
        public bool ShowInPortal { get; set; }
        public bool SuggestedContent { get; set; }
        public ThumbResponse<long> AdminThumb { get; set; }
    }
}
