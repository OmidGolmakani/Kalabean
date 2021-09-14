using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.City;
using Kalabean.Domain.Responses;
using Kalabean.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.Controllers
{
    
    public class ProfileController : Controller
    {
        public ProfileController() { }

        public async Task<IActionResult> UserStore(string name, int id)
        {
            return View();
        }
        public async Task<IActionResult> Cart(string name, int id)
        {
            return View();
        }
    }
}
