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
    /// 检查当前访问的设备是否是手机,如果是跳转到手机路由
    /// </summary>
    public class IsMobileAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 执行action请求之前根据设备的类型, 跳转到不同的路由
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext cntx)
        {
            base.OnActionExecuting(cntx);

            bool isMobile = cntx.HttpContext.Request.Browser.IsMobileDevice;
            if (isMobile)
            {
                cntx.Result = new RedirectResult("/index_m/index");
            }
        }
    }
}