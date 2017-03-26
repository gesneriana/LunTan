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
            using (WeiQingEntities db = new WeiQingEntities())
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

        /// <summary>
        /// 保存八字简批预测的结果
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult SaveYuCe(bazijianpi model)
        {
            if (model != null && model.id > 0)
            {
                using(WeiQingEntities db=new WeiQingEntities())
                {
                    var jp = db.bazijianpi.Where(x => x.id == model.id).FirstOrDefault();
                    if (jp != null && jp.id > 0)
                    {
                        jp.bazi = model.bazi;
                        jp.yuce_content = model.yuce_content;
                        jp.state = model.state;
                        int res= db.SaveChanges();
                        return Content(res.ToString());
                    }
                }
            }
            return Content("-1");
        }
    }
}