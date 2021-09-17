using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface ISMSService
    {
        Task<string> SendPattern(string Code, string ToNum);
    }
}
