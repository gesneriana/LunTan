using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeiQingBaZiFengShuiSuanMing.Controllers_m
{
    /// <summary>
    /// 手机版的网站的控制器
    /// </summary>
    [Filters.IsDesktop]
    public class IndexController : Controller
    {
        public ActionResult index()
        {
            return View("index_m");
        }
    }
}