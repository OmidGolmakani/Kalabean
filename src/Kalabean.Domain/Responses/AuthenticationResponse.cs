using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class AuthenticationResponse
    {
        public string SecretKey { get; set; }
        public int ExpiryTime { get; set; }
        public List<string> ValidIssuers { get; set; }
        public string ValidAudience { get; set; }
        public GoogleResponse Google { get; set; }
    }

}
