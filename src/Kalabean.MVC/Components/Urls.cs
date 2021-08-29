using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Web;

namespace Kalabean.MVC
{
    public static class Urls
    {
        //public static string BaseUrl
        //{
        //    get
        //    {
        //        string scheme = HttpContext.Current.Request.Url.Scheme;
        //        string port = ":" + HttpContext.Current.Request.Url.Port;
        //        string host = HttpContext.Current.Request.Url.Host;
        //        return string.Format("{0}://{1}{2}",
        //            scheme, host, host.StartsWith("localhost") ? port : "");
        //    }
        //}

        public static string BaseUrl(this IUrlHelper url) =>
            string.Format("{0}://{1}",
                url.ActionContext.HttpContext.Request.Scheme,
                url.ActionContext.HttpContext.Request.Host);

        public static string CreateAbsoluteUrl(this IUrlHelper url, string absolutePath)
        {
            if (!string.IsNullOrEmpty(absolutePath))
            {
                if (absolutePath.StartsWith("/"))
                    absolutePath = absolutePath.TrimStart('/');
                if (absolutePath.EndsWith("/"))
                    absolutePath = absolutePath.TrimEnd('/');
            }
            return string.Format("{0}/{1}", BaseUrl(url),absolutePath);
        }

        public static string Home(this IUrlHelper url)
        {
            return CreateAbsoluteUrl(url, url.RouteUrl("home"));
        }
        public static string ShoppingCenters(this IUrlHelper url, string typeName, int typeId)
        {
            return CreateAbsoluteUrl(url, url.RouteUrl("ShoppingCenters", new {
                typeName = typeName,
                typeId = typeId
            }));
        }
    }
}
