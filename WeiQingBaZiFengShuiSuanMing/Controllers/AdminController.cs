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

        /// <summary>
        /// 简批八字的视图页
        /// </summary>
        /// <returns></returns>
        public ActionResult JianPi(int id = 0)
        {
            if (id == 0)
            {
                return Redirect("/admin/index");
            }
            using (WeiQingEntities db = new WeiQingEntities())
            {
                var m = db.bazijianpi.Where(x => x.id == id).FirstOrDefault();
                if (m != null && m.id > 0)
                {
                    ViewData["m"] = m;
                    return View();
                }
            }
            return Redirect("/admin/index");
        }
    }
}