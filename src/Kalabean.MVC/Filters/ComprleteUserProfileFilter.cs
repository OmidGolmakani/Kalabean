using Kalabean.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.MVC.Filters
{
    public class ComprleteUserProfileFilter : ActionFilterAttribute, IAllowAnonymous
    {

        public ComprleteUserProfileFilter()
        {
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
            var UserId = Kalabean.Infrastructure.Helpers.JWTTokenManager.GetUserIdByCookie();
            if (UserId <= 0)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        controller = "Users",
                        action = "Login"
                    }));
                return;
            }
            var entity = await user.GetUserAsync(new Domain.Requests.User.GetUserRequest()
            {
                Id = UserId
            });
            if (entity == null)
            {
                context.Result = new RedirectToRouteResult(
                      new RouteValueDictionary(new
                      {
                          controller = "Users",
                          action = "Login"
                      }));
                return;
            }
            var userRoles = await user.GetUserRoles(new Domain.Requests.User.GetUserRequest() { Id = UserId });
            if (userRoles.Count(u => u == "Administrator") == 1)
            {
                await base.OnActionExecutionAsync(context, next);
                return;
            }
            if (string.IsNullOrEmpty(entity.Address) ||
                entity.EmailConfirmed == false ||
                entity.PhoneNumberConfirmed == false ||
                string.IsNullOrEmpty(entity.Name) ||
                string.IsNullOrEmpty(entity.Family) ||
                string.IsNullOrEmpty(entity.IdCardNo) ||
                string.IsNullOrEmpty(entity.NationalCode) ||
                entity.Subscriptiontype <= 0)
            {
                context.Result = new RedirectToRouteResult(
                      new RouteValueDictionary(new
                      {
                          controller = "Users",
                          action = "Profile"
                      }));
                return;
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
