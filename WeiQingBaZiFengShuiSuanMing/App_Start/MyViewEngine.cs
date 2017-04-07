using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeiQingBaZiFengShuiSuanMing.App_Start
{
    /// <summary>
    /// 自定义的视图寻址规则, 这里没有加上分区视图, 以及 aspx vbhtml 等
    /// </summary>
    public sealed class MyViewEngine : RazorViewEngine
    {
        /// <summary>
        /// 先搜索pc版视图
        /// </summary>
        public static string[] fmt1 = new string[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/m/{1}/{0}.cshtml"
            };

        /// <summary>
        /// 先搜索移动版视图
        /// </summary>
        public static string[] fmt2 = new string[]
            {
                "~/Views/m/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };

        /// <summary>
        /// 最最后搜索 手机版的视图
        /// </summary>
        public MyViewEngine()
        {
            ViewLocationFormats = fmt1;
        }

        /// <summary>
        /// 重写视图寻址规则, 主要就是重新设置 视图寻址路径格式
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="viewName"></param>
        /// <param name="masterName"></param>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            // 由于IIS对于不同的控制器不能正常区分, 所以只能用不同的控制器名称   
            return base.FindView(controllerContext, viewName, masterName, useCache);
        }

    }
}