using Kalabean.Domain.Services;
using Kalabean.Infrastructure;
using Kalabean.Infrastructure.Helpers;
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
    public class CustomAuthorizationFilter : ActionFilterAttribute, IAllowAnonymous
    {
        public CustomAuthorizationFilter()
        {
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            bool hasAllowAnonymous = context.ActionDescriptor.EndpointMetadata
                               .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));
            if (!hasAllowAnonymous)
            {
                if (context.HttpContext.Request.Cookies.Count(x => x.Key == "AccessToken") == 0)
                {
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new
                        {
                            controller = "User",
                            action = "Login"
                        }));
                }
                string token = "";
                token = context.HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == "AccessToken").Value;


                var dbContext = context.HttpContext.RequestServices.GetRequiredService<AppDbContext>();
                string User = "";
                try
                {
                    User = JWTTokenManager.ValidateToken(token, dbContext);
                }
                catch (Exception)
                {
                    context.Result = new RedirectToRouteResult(
                       new RouteValueDictionary(new
                       {
                           controller = "Users",
                           action = "Login"
                       }));
                }
                if (User == null)
                {
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new
                        {
                            controller = "Users",
                            action = "Login"
                        }));
                }
            }
            base.OnResultExecuting(context);
        }
    }
}

