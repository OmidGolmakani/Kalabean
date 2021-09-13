using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.City;
using Kalabean.Domain.Responses;
using Kalabean.Domain.Services;
using Kalabean.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.API.Controllers
{

    [Route("Login")]
    public class LoginController : Controller
    {
        private readonly IUserService _user;

        public LoginController(IUserService user)
        {
            this._user = user;
        }
        public async Task<IActionResult> Index()
        {
            LoginViewModel model = new LoginViewModel();
            return View("Login", model);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Domain.Requests.User.LoginRequest request)
        {
            var Result = await _user.SignIn(request);
            if (Result.SignIn.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتبا می باشد");
            LoginViewModel model = new LoginViewModel();
            model.UserName = request.UserName;
            model.Password = request.Password;
            return View("Login",model);
        }
    }
}
