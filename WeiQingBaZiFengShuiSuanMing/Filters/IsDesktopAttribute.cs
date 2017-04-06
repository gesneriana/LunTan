using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

/// <summary>
/// MVC拦截器的命名空间
/// </summary>
namespace WeiQingBaZiFengShuiSuanMing.Filters
{
    /// <summary>
    /// 检查当前访问的设备是否是桌面PC,如果是,直接跳转到电脑版
    /// </summary>
    public class IsDesktopAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 执行action请求之前根据设备的类型 重定向跳转到不同的路由中
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext cntx)
        {
            base.OnActionExecuting(cntx);

            bool isMobile = cntx.HttpContext.Request.Browser.IsMobileDevice;
            if (!isMobile)
            {
                cntx.Result = new RedirectResult("/index/index");
            }
        }
    }
}