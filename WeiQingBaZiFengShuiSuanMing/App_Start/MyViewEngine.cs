using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeiQingBaZiFengShuiSuanMing.App_Start
{
    /// <summary>
    /// 自定义的视图寻址规则
    /// </summary>
    public sealed class MyViewEngine : RazorViewEngine
    {
        /// <summary>
        /// 最最后搜索 手机版的视图
        /// </summary>
        public MyViewEngine()
        {
            ViewLocationFormats = new[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/m/{1}/{0}.cshtml"//我们的规则
            };
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            return base.FindView(controllerContext, viewName, masterName, useCache);
        }

    }
}