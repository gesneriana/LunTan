using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeiQingBaZiFengShuiSuanMing.Filters
{
    /// <summary>
    /// 当用户进行一些修改数据的操作的时候,验证用户是否已经登录
    /// 默认为Ajax发送的post请求
    /// </summary>
    public class CheckLoginAttribute : ActionFilterAttribute
    {
        private bool isAjax = true;
        private bool isPost = true;
        private bool isLogin = true;
        
        /// <summary>
        /// 默认为post请求
        /// </summary>
        public bool IsPost { get { return isPost; } set { isPost = value; } }

        /// <summary>
        /// 默认为Ajax请求
        /// </summary>
        public bool IsAjax { get { return isAjax; } set { isAjax = value; } }

        /// <summary>
        /// 默认为检查用户登录为true
        /// </summary>
        public bool IsLogin { get { return isLogin; } set { isLogin = value; } }

        /// <summary>
        /// 执行action请求之前判断用户是否登录
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext cntx)
        {
            base.OnActionExecuting(cntx);

            if (isPost)
            {
                if (!"POST".Equals(cntx.HttpContext.Request.HttpMethod))
                {
                    cntx.Result = new EmptyResult();    // 中断请求
                    return;
                }
            }

            if (isAjax)
            {
                if (!cntx.HttpContext.Request.IsAjaxRequest())
                {
                    cntx.Result = new EmptyResult();    // 中断请求
                    return;
                }
            }

            if (isLogin && cntx.HttpContext.Session["user"] == null)
            {
                if (cntx.HttpContext.Request.IsAjaxRequest())
                {
                    cntx.Result = new ContentResult() { Content = "你没有登录" };
                    return;
                }
                cntx.Result = new RedirectResult("/index/index");
            }
        }
    }
}