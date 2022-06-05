using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrionMaster.Models.Globalization
{
    public class GlobalizeFilterAttribute : ActionFilterAttribute
    {
        // Define language in current context 
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Get current Http
            
            HttpContext context = filterContext.HttpContext;
            //if sent by Url 

            //string cultureName = context.Request.QueryString["lang"];
            string cultureName = context.Request.Query["lang"];
            //var queryString = context.Request.Query;
            //StringValues cultureName;
            //queryString.TryGetValue("lang", out cultureName);
            //Cookie test 


            //DateTime localTime1 = DateTime.Now.AddDays(1);
            //localTime1 = DateTime.SpecifyKind(localTime1, DateTimeKind.Local);
            //DateTimeOffset localTime2 = localTime1;


            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);
            if (string.IsNullOrEmpty(cultureName))
            {
                cultureName = (null != context.Request.Cookies["lang3"]) ? context.Request.Cookies["lang3"].ToString() : string.Empty;
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
                    context.Response.Cookies.Append("lang3", cultureName);
                }
            }
            else
            {
                context.Response.Cookies.Append("lang3", cultureName);
            }

            // Change culture on current thread 
            CultureInfo culture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            //action continuation 
            base.OnActionExecuting(filterContext);
        }
    }
}