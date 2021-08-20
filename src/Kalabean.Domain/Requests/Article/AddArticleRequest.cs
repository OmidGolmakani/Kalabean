using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.Article
{
    public class AddArticleRequest
    {
        public string Name { get; set; }
        public string KeyWords { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string HtmlBody { get; set; }
        public DateTime? PublishDateTime { get; set; }
        public DateTime? ArchiveDateTime { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile File { get; set; }
        public bool ShowInPortal { get; set; }
        public bool SuggestedContent { get; set; }
    }
}
