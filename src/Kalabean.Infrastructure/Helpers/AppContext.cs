using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Helpers
{
    public static class MyAppContext
    {
        private static HttpContext _context;

        public static void Configure()
        {
            //_context = new HttpContextAccessor().HttpContext;
        }

        public static HttpContext Current => _context;
    }
}
