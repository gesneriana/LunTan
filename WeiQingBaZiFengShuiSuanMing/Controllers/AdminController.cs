using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDao.Entity;

namespace WeiQingBaZiFengShuiSuanMing.Controllers
{
    [Filters.Admin]
    public class AdminController : Controller
    {
        /// <summary>
        /// 显示管理员的后台页面
        /// </summary>
        /// <returns></returns>
        public ActionResult index()
        {
            using(WeiQingEntities db=new WeiQingEntities())
            {
                var baziList = db.bazijianpi.Where(p => p.state == 0).OrderBy(p => p.addtime).ToList();
                ViewData["baziList"] = baziList;
            }
            return View();
        }
    }
}