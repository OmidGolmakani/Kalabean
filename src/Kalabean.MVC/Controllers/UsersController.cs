﻿using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.City;
using Kalabean.Domain.Requests.User;
using Kalabean.Domain.Responses;
using Kalabean.Domain.Services;
using Kalabean.Infrastructure.AppSettingConfigs;
using Kalabean.Infrastructure.Helpers;
using Kalabean.MVC.Filters;
using Kalabean.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.API.Controllers
{

    [Route("User")]
    public class UsersController : Controller
    {
        private readonly IUserService _user;

        public UsersController(IUserService user)
        {
            this._user = user;
        }
        [Route("Login")]
        public async Task<IActionResult> Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View("Login", model);
        }
        [Route("Profile")]
        [CustomAuthorizationFilter]
        public async Task<IActionResult> Profile()
        {
            var model = new UserProfileViewModel(Kalabean.Infrastructure.Helpers.ReturnFilePath.BaseUrl);
            var user = await _user.GetUserAsync(new GetUserRequest() { Id = JWTTokenManager.GetUserIdByCookie() });
            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }
            model.Id = user.Id;
            model.UserName = user.UserName;
            model.Address = user.Address;
            model.Name = user.Name;
            model.Family = user.Family;
            model.Email = user.Email;
            model.IdCard = user.IdCardNo;
            model.NationalCode = user.NationalCode;
            model.SubscriptionType = user.Subscriptiontype;
            model.UserStatus = user.UserStatus;
            model.PhoneNumber = user.PhoneNumber;
            return View(model);
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Domain.Requests.User.LoginRequest request)
        {
            request.UseApi = false;
            var Result = await _user.SignIn(request);
            if (Result.SignIn.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتبا می باشد");
            LoginViewModel model = new LoginViewModel();
            model.UserName = request.UserName;
            model.Password = request.Password;
            return View("Login", model);
        }
        [HttpPost("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(Domain.Requests.User.EditUserRequest request)
        {
            await _user.EditUserAsync(request);
            return RedirectToAction("Profile");
        }
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(Domain.Requests.User.AddUserRequest request)
        {
            request.UserName = request.PhoneNumber;
            request.Password = request.PhoneNumber;
            await _user.AddUserAsync(request);
            await _user.SignIn(new LoginRequest() { UserName = request.UserName, Password = request.Password });
            return RedirectToAction("Profile");
        }
        [HttpGet("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            var model = new UserProfileViewModel(Kalabean.Infrastructure.Helpers.ReturnFilePath.BaseUrl);
            return View(model);
        }
    }
}
