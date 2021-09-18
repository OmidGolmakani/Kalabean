using Kalabean.Domain.Entities;
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
        private readonly ISMSService _sms;

        public UsersController(IUserService user,
                               ISMSService sms)
        {
            this._user = user;
            this._sms = sms;
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
            var user = await _user.GetUsersAsync(new GetUsersRequest() { UserName = request.UserName });
            if (user.Total == 0)
            {
                ModelState.AddModelError("", "کاربر مورد نظر یافت نشد");
                return View();
            }
            if (user.Items.FirstOrDefault().PhoneNumberConfirmed == false)
            {
                return RedirectToAction("SendVerificationCode", new
                {
                    PhoneNumber = user.Items.FirstOrDefault().PhoneNumber
                });
            }
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
            //await _user.SignIn(new LoginRequest() { UserName = request.UserName, Password = request.Password });
            //return RedirectToAction("Profile");
            return RedirectToAction("SendVerificationCode", new
            {
                PhoneNumber = request.PhoneNumber
            });
        }
        [HttpGet("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            var model = new UserProfileViewModel(Kalabean.Infrastructure.Helpers.ReturnFilePath.BaseUrl);
            return View(model);
        }
        [HttpGet("SendVerificationCode")]
        [AllowAnonymous]
        public async Task<IActionResult> SendVerificationCode(string PhoneNumber)
        {
            var model = new Kalabean.MVC.Models.VerifyViewModel();
            model.PhoneNumber = PhoneNumber;
            return View("VerificationCode", model);
        }
        [HttpPost("PostVerificationCode")]
        [AllowAnonymous]
        public async Task<IActionResult> PostVerificationCode(string PhoneNumber)
        {
            var model = new Kalabean.MVC.Models.VerifyViewModel();
            try
            {
                var Code = await _user.PhoneNumberConfirmation(PhoneNumber);
                await _sms.SendPattern(Code, PhoneNumber);
            }
            catch (Exception ex) { ModelState.AddModelError(string.Empty, ex.Message); }
            model.PhoneNumber = PhoneNumber;
            model.VerifyType = 1;
            return View("VerificationCode", model);
        }
        [HttpPost("VerificationCode")]
        [AllowAnonymous]
        public async Task<IActionResult> VerificationCode(VerifyViewModel model)
        {
            var result = await _user.VerifyPhoneNumber(model.PhoneNumber, model.VerificationCode);
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ModelState.AddModelError("", "کد فعال سازی نامعتبر می باشد");
                return View();
            }
        }
    }
}
