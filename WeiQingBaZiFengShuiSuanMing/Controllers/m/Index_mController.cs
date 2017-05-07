using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDao.Entity;
using Common.Utility;

namespace WeiQingBaZiFengShuiSuanMing.Controllers.m
{
    /// <summary>
    /// 手机版的网站的控制器
    /// </summary>
    [Filters.IsDesktop]
    public class Index_mController : Controller
    {
        public ActionResult index(string key = "")
        {
            using (bazifengshuisuanmingEntities db = new bazifengshuisuanmingEntities())
            {
                if (!string.IsNullOrEmpty(key))
                {
                    var ycList = db.bazijianpi.Where(x => x.state == 1 && (x.name.Contains(key) || x.born_place.Contains(key) || x.bazi.Contains(key))).OrderByDescending(x => x.addtime).Take(10).ToList();
                    ViewData["ycList"] = ycList;
                }
                else
                {
                    var ycList = db.bazijianpi.Where(x => x.state == 1).OrderByDescending(x => x.addtime).Take(10).ToList();
                    ViewData["ycList"] = ycList;
                }
            }
            return View();
        }

        public ActionResult yclist(int page=1,string key = "")
        {
            using (bazifengshuisuanmingEntities db = new bazifengshuisuanmingEntities())
            {
                if (!string.IsNullOrEmpty(key))
                {
                    // var ycList = db.bazijianpi.Where(x => x.state == 1 && (x.name.Contains(key) || x.born_place.Contains(key) || x.bazi.Contains(key))).OrderByDescending(x => x.addtime).Take(10).ToList();
                    var q = db.bazijianpi.Where(x => x.state == 1 && (x.name.Contains(key) || x.born_place.Contains(key) || x.bazi.Contains(key))).OrderByDescending(x => x.addtime);
                    var p = new EFPaging<bazijianpi>();
                    var yclist = p.getPageList(q, "/index_m/yclist", page, 12);
                    ViewData["url"] = p.pageUrl;
                    ViewData["ycList"] = yclist;
                }
                else
                {
                    var q = db.bazijianpi.Where(x => x.state == 1).OrderByDescending(x => x.addtime);
                    var p = new EFPaging<bazijianpi>();
                    var yclist = p.getPageList(q, "/index_m/yclist", page, 12);
                    ViewData["url"] = p.pageUrl;
                    ViewData["ycList"] = yclist;
                }
            }
            return View();
        }
    }
}