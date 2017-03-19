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
    /// 检查当前访问的设备是否是手机,将不同的视图名称放在ViewData["act"].ToString() 中
    /// </summary>
    public class IsMobileAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 执行action请求之前根据设备的类型返回不同的视图,从 ViewData["act"].ToString() 中读取不同的视图名称
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext cntx)
        {
            base.OnActionExecuting(cntx);

            string action = cntx.RouteData.Values["action"].ToString();
            bool isMobile = cntx.HttpContext.Request.Browser.IsMobileDevice;
            if (isMobile)
            {
                cntx.Controller.ViewData["act"] = action + "_m";
            }
            else
            {
                cntx.Controller.ViewData["act"] = action;
            }
        }
    }
}