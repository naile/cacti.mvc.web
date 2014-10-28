using System;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;

namespace Cacti.Mvc.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class AutoRefreshAttribute : ActionFilterAttribute
    {
        public const int DefaultDurationInSeconds = 300; // 5 Minutes

        public AutoRefreshAttribute()
        {
            DurationInSeconds = DefaultDurationInSeconds;
        }

        public int DurationInSeconds { get; set; }

        public string RouteName { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var url = BuildUrl(filterContext);
            var headerValue = string.Concat(DurationInSeconds, ";Url=", url);

            filterContext.HttpContext.Response.AppendHeader("Refresh", headerValue);

            base.OnResultExecuted(filterContext);
        }

        private string BuildUrl(ControllerContext filterContext)
        {
            var urlHelper = new UrlHelper(filterContext.RequestContext);
            string url;

            if (!string.IsNullOrEmpty(RouteName))
            {
                url = urlHelper.RouteUrl(RouteName);
            }
            else if (!string.IsNullOrEmpty(ControllerName) && !string.IsNullOrEmpty(ActionName))
            {
                url = urlHelper.Action(ActionName, ControllerName);
            }
            else if (!string.IsNullOrEmpty(ActionName))
            {
                url = urlHelper.Action(ActionName);
            }
            else
            {
                url = filterContext.HttpContext.Request.RawUrl;
            }

            return url;
        }
    }
}
