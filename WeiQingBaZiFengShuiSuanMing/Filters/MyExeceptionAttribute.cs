using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeiQingBaZiFengShuiSuanMing.Filters
{
    public class MyExeceptionAttribute : HandleErrorAttribute
    {

        /// <summary>
        /// 控制器方法中出现异常，会调用该方法捕获异常
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            // filterContext.HttpContext.Response.Redirect("/Error.html");
            filterContext.Result = new ContentResult() { Content = filterContext.Exception.Message };
            base.OnException(filterContext);
        }
    }
}