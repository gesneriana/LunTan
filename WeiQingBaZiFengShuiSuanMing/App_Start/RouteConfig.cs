using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WeiQingBaZiFengShuiSuanMing
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // 手机版的路由
            routes.MapRoute(
                name: "mobile",
                url: "m/{controller}/{action}/{id}",
                defaults: new { controller = "Index", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WeiQingBaZiFengShuiSuanMing.Controllers_m" }
            );

            // 外网默认访问的路由
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Index", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WeiQingBaZiFengShuiSuanMing.Controllers" }
            );
        }
    }
}
