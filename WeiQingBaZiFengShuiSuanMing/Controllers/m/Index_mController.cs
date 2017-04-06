using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeiQingBaZiFengShuiSuanMing.Controllers.m
{
    /// <summary>
    /// 手机版的网站的控制器
    /// </summary>
    [Filters.IsDesktop]
    public class Index_mController : Controller
    {
        public ActionResult index()
        {
            return View();
        }
    }
}