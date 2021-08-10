using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Helpers
{
    internal static class AppContext
    {
        private static IHttpContextAccessor _httpContextAccessor;

        internal static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        internal static HttpContext Current => _httpContextAccessor.HttpContext;
    }
}
