using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.SMS
{
    public class SendPatternRequest
    {
        public string op
        {
            get
            {
                return "patternV2";
            }
        }
        public string user
        {
            get
            {
                return "kalabean";
            }
        }
        public string pass
        {
            get
            {
                return "k9158990160";
            }
        }
        public string fromNum
        {
            get
            {
                return "10009158990160";
            }
        }
        public string toNum { get; set; }
        public string patternCode
        {
            get
            {
                return "6302k8s915";
            }
        }
        public InputData inputData { get; set; }
        public string ConvertToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
