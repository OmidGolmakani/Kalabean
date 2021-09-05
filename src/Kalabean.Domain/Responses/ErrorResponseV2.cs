using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class ErrorResponseV2
    {
        public int StatusCode { get; set; }
        public List<string> MsgErrors { get; set; } = new List<string>();
        public string JsonConvert()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
