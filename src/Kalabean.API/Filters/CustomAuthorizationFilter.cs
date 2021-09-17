using Kalabean.Domain.Responses;
using Kalabean.Domain.Services;
using Kalabean.Infrastructure;
using Kalabean.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.Filter
{
    public class CustomAuthorizationFilter : ActionFilterAttribute, IAllowAnonymous //, IAuthorizationFilter, IAllowAnonymous, IAuthorizeData, IActionFilter
    {

        public CustomAuthorizationFilter()
        {
        }
        private string _role = "";
        public CustomAuthorizationFilter(string Role)
        {
            _role = Role;
        }
        public string AuthenticationSchemes { get; set; }
        public string Policy { get; set; }
        public string Roles { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            IWebHostEnvironment env = context.HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
            if (env.EnvironmentName == "Development")
            {
                return;
            }
            ErrorResponse Err = null;
            bool hasAllowAnonymous = context.ActionDescriptor.EndpointMetadata
                                .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));
            if (!hasAllowAnonymous)
            {
                var Result = new JsonResult(new ErrorResponse());
                Result.ContentType = "application/json";
                if (context.HttpContext.Request.Headers["Authorization"].ToString().StartsWith("Bearer") == false)
                {
                    context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    Err = new ErrorResponse()
                    {
                        Message = "توکن ارسال نشده است",
                        StatusCode = context.HttpContext.Response.StatusCode
                    };

                    Result.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    Result.Value = Err;
                    context.Result = Result;
                    return;
                }
                var token = context.HttpContext.Request.Headers["Authorization"].ToString();
                if (token == "")
                {
                    context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    Err = new ErrorResponse()
                    {
                        Message = "توکن ارسال نشده است",
                        StatusCode = context.HttpContext.Response.StatusCode
                    };

                    Result.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    Result.Value = Err;
                    context.Result = Result;
                    return;
                }
                token = token.Substring(6, token.Length - 6).Trim();


                var dbContext = context.HttpContext.RequestServices.GetRequiredService<AppDbContext>();

                var User = JWTTokenManager.ValidateToken(token, dbContext);
                if (User == null)
                {
                    var AccountService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                    AccountService.SignOut();
                    context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    Err = new ErrorResponse()
                    {
                        Message = "توکن ارسال شده نا معتبر است",
                        StatusCode = context.HttpContext.Response.StatusCode
                    };
                    Result.Value = Err;
                    Result.StatusCode = context.HttpContext.Response.StatusCode;
                    context.Result = Result;
                    return;
                }
                bool IsAdmin = true;
                //IsAdmin = true ? (from u in dbContext.Users
                //                  join ur in dbContext.UserRoles
                //                  on u.Id equals ur.UserId
                //                  join r in dbContext.Roles
                //                  on ur.RoleId equals r.Id
                //                  where u.UserName == User && r.Name == "Administrator"
                //                  select 1).Count() != 0 : false;
                if (IsAdmin == false)
                {
                    var Route = context.RouteData;
                    string CurrentController = Route.Values["Controller"].ToString();
                    string CurrentAction = Route.Values["Action"].ToString();
                    string Url = string.Format("/api/{0}/{1}", CurrentController, CurrentAction);
                    var p = dbContext.RolePermissions.FirstOrDefault(x => x.Url == Url);
                    if (p == null)
                    {
                        context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                        Err = new ErrorResponse()
                        {
                            Message = "دسترسی شما به این آدرس وجود ندارد",
                            StatusCode = context.HttpContext.Response.StatusCode
                        };
                        Result.Value = Err;
                        Result.StatusCode = context.HttpContext.Response.StatusCode;
                        context.Result = Result;
                        return;
                    }
                    p.Id = 0;
                    if ((JWTTokenManager.ValidatePermissionToken(p.Token) == null) ||
                        (!JWTTokenManager.ValidatePermissionToken(p.Token).Equals(p.Url))
                        )
                    {
                        context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                        Err = new ErrorResponse()
                        {
                            Message = "دسترسی شما به این آدرس وجود ندارد",
                            StatusCode = context.HttpContext.Response.StatusCode
                        };
                        context.Result = new JsonResult(Err);
                        return;
                    }
                }
            }
            base.OnActionExecuting(context);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return base.OnActionExecutionAsync(context, next);
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }
        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            return base.OnResultExecutionAsync(context, next);
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
        }

    }
}
