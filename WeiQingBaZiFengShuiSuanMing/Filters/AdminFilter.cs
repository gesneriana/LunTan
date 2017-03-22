using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeiQingBaZiFengShuiSuanMing.Filters
{
    /// <summary>
    /// 管理员操作的拦截器
    /// </summary>
    public class AdminAttribute : ActionFilterAttribute
    {

        /// <summary>
        /// 验证用户的管理员权限
        /// </summary>
        /// <param name="cntx"></param>
        public override void OnActionExecuting(ActionExecutingContext cntx)
        {
            base.OnActionExecuting(cntx);

            var user = (EFDao.Entity.user)cntx.HttpContext.Session["user"];
            if (user == null)
            {
                if (cntx.HttpContext.Request.IsAjaxRequest())
                {
                    cntx.Result = new ContentResult() { Content = "你没有登录" };
                    return;
                }
                cntx.Result = new RedirectResult("/index/adminlogin");
                return;
            }
            if (!user.is_admin)
            {
                if (cntx.HttpContext.Request.IsAjaxRequest())
                {
                    cntx.Result = new ContentResult() { Content = "你没有管理员权限" };
                    return;
                }
                cntx.Result = new RedirectResult("/index/adminlogin");
                return;
            }
        }

    }
}