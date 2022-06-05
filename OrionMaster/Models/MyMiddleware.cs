using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrionMaster.Models
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;

        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            //HttpContext context = filterContext.HttpContext;
            //if sent by Url 

            //string cultureName = context.Request.QueryString["lang"];
            string cultureName = context.Request.Query["lang"];
            //var queryString = context.Request.Query;
            //StringValues cultureName;
            //queryString.TryGetValue("lang", out cultureName);
            //Cookie test 

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);
            if (string.IsNullOrEmpty(cultureName))
            {
                cultureName = (null != context.Request.Cookies["lang5"]) ? context.Request.Cookies["lang5"].ToString() : string.Empty;
                if (string.IsNullOrEmpty(cultureName))
                {
                    try
                    {
                        cultureName = context.Request.Headers["Accept-Language"].ToString().Split(";").FirstOrDefault()?.Split(",").FirstOrDefault();
                        if (cultureName.Contains("ar"))
                            cultureName = "Ar";
                        else
                            cultureName = "En";
                        if (string.IsNullOrEmpty(cultureName)) cultureName = "En";
                    }
                    catch { cultureName = "En"; }
                    context.Response.Cookies.Append("lang5", cultureName,option);
                }
            }
            else
            {
                context.Response.Cookies.Append("lang5", cultureName,option);
            }

            // Change culture on current thread 
            CultureInfo culture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            return _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware>();
        }
    }
}
