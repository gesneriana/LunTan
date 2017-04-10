using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDao.Entity;
using Common.Utility;
using Entity;

namespace WeiQingBaZiFengShuiSuanMing.Controllers
{
    [Filters.Admin]
    public class AdminController : Controller
    {
        /// <summary>
        /// 显示管理员的后台页面
        /// </summary>
        /// <returns></returns>
        public ActionResult index(string key = "", int page = 1)
        {
            using (WeiQingEntities db = new WeiQingEntities())
            {
                var efp = new EFPaging<bazijianpi>();
                if (key != null && key.Length > 0)
                {
                    var bl = (from q in db.bazijianpi
                              where (q.name.Contains(key) || q.born_place.Contains(key) || q.bazi.Contains(key))
                              orderby q.state, q.addtime
                              select q);  // 优先查看待简批的八字

                    ViewData["baziList"] = efp.getPageList(bl, "/admin/index", page, 10);
                    ViewData["url"] = efp.pageUrl;
                }
                else
                {
                    var bl = (from q in db.bazijianpi
                              orderby q.state, q.addtime
                              select q);  // 优先查看待简批的八字
                    ViewData["baziList"] = efp.getPageList(bl, "/admin/index", page, 10);
                    ViewData["url"] = efp.pageUrl;
                }

                var notice = db.notice.OrderByDescending(x => x.addtime).FirstOrDefault();
                ViewData["notice"] = notice;

                ViewData["cateList"] = EfExt.selectAll<category>();
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
                reflectModel.setValues(model);
                using (WeiQingEntities db = new WeiQingEntities())
                {
                    var jp = db.bazijianpi.Where(x => x.id == model.id).FirstOrDefault();
                    if (jp != null && jp.id > 0)
                    {
                        jp.bazi = model.bazi;
                        jp.yuce_content = model.yuce_content;
                        jp.state = model.state;
                        int res = db.SaveChanges();
                        return Content(res.ToString());
                    }
                }
            }
            return Content("-1");
        }

        /// <summary>
        /// 发布公告
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult fbgg(notice model)
        {
            if (model != null && model.content != null && model.content.Length > 0)
            {
                var user = (user)Session["user"];
                using (WeiQingEntities db = new WeiQingEntities())
                {
                    var count = db.notice.Count();
                    if (count == 0)
                    {
                        model.addtime = DateTime.Now;
                        model.uid = (int)user.id;
                        db.notice.Add(model);
                        return Content(db.SaveChanges().ToString());
                    }
                    else
                    {
                        var notice = db.notice.OrderByDescending(x => x.addtime).FirstOrDefault();
                        if (notice != null && notice.id > 0)
                        {
                            notice.show = model.show;
                            notice.content = model.content;
                            return Content(db.SaveChanges().ToString());
                        }
                        else
                        {
                            model.addtime = DateTime.Now;
                            model.uid = (int)user.id;
                            db.notice.Add(model);
                            return Content(db.SaveChanges().ToString());
                        }
                    }
                }
            }
            return Content("参数错误");
        }

        /// <summary>
        /// 用户当前论坛注册用户的列表,包括被禁止的用户
        /// </summary>
        /// <param name="key">查询条件</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        public ActionResult userList(string key = "", int page = 1)
        {
            using (WeiQingEntities db = new WeiQingEntities())
            {
                if (key != null && key.Length > 0)
                {
                    var q = db.user.Where(x => x.nick_name.Contains(key) || x.email.Contains(key) || x.mobile.Contains(key) || x.qq.Contains(key) || x.wei_xin.Contains(key)).OrderByDescending(x => x.is_admin).ThenBy(x => x.state).ThenByDescending(x => x.reg_date);
                    var p = new EFPaging<user>();
                    var ulist = p.getPageList(q, "/index/userList", page, 20);
                    ViewData["ulist"] = ulist;
                    ViewData["url"] = p.pageUrl;
                }
                else
                {
                    var q = db.user.OrderByDescending(x => x.is_admin).ThenBy(x => x.state).ThenByDescending(x => x.reg_date);
                    var p = new EFPaging<user>();
                    var ulist = p.getPageList(q, "/index/userList", page, 20);
                    ViewData["ulist"] = ulist;
                    ViewData["url"] = p.pageUrl;
                }
            }
            return View();
        }

        /// <summary>
        /// 显示指定用户的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult user_edit(int id = 0)
        {
            if (id > 0)
            {
                using (WeiQingEntities db = new WeiQingEntities())
                {
                    var u = db.user.Where(x => x.id == id).FirstOrDefault();
                    if (u != null && u.id > 0)
                    {
                        ViewData["u"] = u;
                        return View();
                    }
                }
            }
            return Content("无数据,没有找到指定用户的资料");
        }

        /// <summary>
        /// 管理员在后台修改用户的资料
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public ActionResult updateUser(UserExt u)
        {
            if (u != null && u.id > 0)
            {
                if (u.id == 1 && (u.state == 0 || u.is_admin == false))
                {
                    return Content("超级管理员的权限不能更改");
                }

                var cur_user = (user)Session["user"];
                if (u.pwd != null && u.pwd.Length >= 6)
                {
                    if (u.id == 1 && cur_user.id != 1)
                    {
                        return Content("超级管理员的密码不能更改");
                    }
                    u.pwd = HashTools.SHA1_Hash(u.pwd);
                }
                else
                {
                    u.pwd = u.oldpwd;
                }

                if (u.email != null && u.email.Length > 0)
                {
                    if (u.reg_date == DateTime.MinValue)
                    {
                        return Content("注册时间参数错误");
                    }
                    reflectModel.setValues(u);
                    try
                    {
                        var model = reflectModel.AutoCopyToBase<user, UserExt>(u);
                        int res = EfExt.Update(model);
                        if (res > 0)
                        {
                            return Content("1");
                        }
                    }
                    catch (Exception ex)
                    {
                        return Content(ex.Message);
                    }

                    return Content("修改失败");
                }
                else
                {
                    return Content("邮箱不能为空");
                }
            }
            return Content("参数错误");
        }

        /// <summary>
        /// 审批列表页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult jubaoshenpi(int page = 1)
        {
            using (WeiQingEntities db = new WeiQingEntities())
            {
                var q = from t in db.tiezi_jubao orderby t.state, t.addtime select t;
                var p = new EFPaging<tiezi_jubao>();
                var list = p.getPageList(q, "/admin/jubaoshenpi", page, 20);
                ViewData["list"] = list;
                ViewData["url"] = p.pageUrl;
            }
            return View();
        }

        /// <summary>
        /// 审批视图页面
        /// </summary>
        /// <param name="id">指定楼层的帖子id,主键</param>
        /// <returns></returns>
        public ActionResult editShenPi(int id = 0, int jbid = 0)
        {
            if (id > 0)
            {
                using (WeiQingEntities db = new WeiQingEntities())
                {
                    var t = (from te in db.tiezi where te.id == id select te).FirstOrDefault();
                    var model = reflectModel.AutoCopyToChild<tiezi, TieZiExt>(t);   // 获取当前id的帖子对象
                    if (model.id > 0)
                    {
                        var list = (from r in db.tzreply where r.tzid == model.id select r).ToList();
                        model.replyList = list;
                        ViewData["model"] = model;
                        ViewData["jbid"] = jbid;
                        if (model != null && model.id > 0)
                        {
                            return View();
                        }
                    }
                }
            }
            return Content("没有数据");
        }

        /// <summary>
        /// 帖子审批
        /// </summary>
        /// <param name="model"></param>
        /// <param name="jbid">举报表的主键</param>
        /// <returns></returns>
        public ActionResult shenPiTieZi(TieZiExt model, int jbid = 0)
        {
            if (model != null && model.id > 0 && jbid > 0)
            {
                using (WeiQingEntities db = new WeiQingEntities())
                {
                    int res = 0;
                    var t = db.tiezi.Where(x => x.id == model.id).FirstOrDefault();
                    if (t != null && t.id > 0)
                    {
                        t.state = model.state;

                        if (model.replyList != null && model.replyList.Count > 0)
                        {
                            var rlist = db.tzreply.Where(x => x.tzid == t.id).ToList();  // 获取帖子的回复列表
                            if (rlist != null && rlist.Count > 0)
                            {
                                var tempdic = model.replyList.ToDictionary(x => x.id);
                                foreach (var item in rlist)
                                {
                                    if (tempdic.ContainsKey(item.id))
                                    {
                                        item.state = tempdic[item.id].state;
                                    }
                                }
                                res += EfExt.UpdateMany(rlist);
                            }
                        }

                        var jb = db.tiezi_jubao.Where(x => x.id == jbid).FirstOrDefault();
                        if (jb != null && jb.id > 0 && jb.tzid == t.id)
                        {
                            jb.state = 1;   // 已审批
                            res += db.SaveChanges();
                            if (res > 0)
                            {
                                return Content("审核成功");
                            }
                            return Content("审核失败");
                        }
                    }
                }
            }
            return Content("参数错误");
        }

        /// <summary>
        /// 发布每日文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult fbart(article model)
        {
            if (model != null)
            {
                reflectModel.setValues(model);
                if (model.content.Length > 0 && model.title.Length > 0)
                {
                    model.addtime = DateTime.MinValue;
                    var user = (user)Session["user"];
                    model.uid = (int)user.id;
                    if (EfExt.insert(model) > 0)
                    {
                        return Content("发布成功");
                    }
                }
                else
                {
                    return Content("请输入内容和标题");
                }
            }
            return Content("发布失败");
        }

        /// <summary>
        /// 站长发布的文章列表,分页显示所有,优先显示 state==1 的文章
        /// </summary>
        /// <param name="key"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult artList(string key = "", int page = 1)
        {
            using (WeiQingEntities db = new WeiQingEntities())
            {
                if (key != null && key.Length > 0)
                {
                    var q = db.article.Where(x => x.title.Contains(key) || x.keywords.Contains(key)).OrderByDescending(x => x.top).ThenBy(x => x.sort).ThenByDescending(x => x.addtime);
                    var p = new EFPaging<article>();
                    var list = p.getPageList(q, "/admin/artList", page, 20);
                    ViewData["list"] = list;
                    ViewData["url"] = p.pageUrl;
                }
                else
                {
                    var q = db.article.OrderByDescending(x => x.top).ThenBy(x => x.sort).ThenByDescending(x => x.addtime);
                    var p = new EFPaging<article>();
                    var list = p.getPageList(q, "/admin/artList", page, 20);
                    ViewData["list"] = list;
                    ViewData["url"] = p.pageUrl;
                }                
                ViewData["cateDic"] = EfExt.selectAll<category>().ToDictionary(x => x.id);
            }
            return View();
        }

        /// <summary>
        /// 修改发布文章的视图页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult edit_art(int id = 0)
        {
            if (id > 0)
            {
                var m = EfExt.select<article>(id);
                if (m != null && m.id > 0)
                {
                    ViewData["model"] = m;
                    return View();
                }
            }
            return Content("无数据");
        }

        /// <summary>
        /// 管理员修改每日发布的文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult edit_article(article model)
        {
            if (model != null && model.id > 0)
            {
                var r = EfExt.update(model);
                if (r > 0)
                {
                    return Content("修改成功");
                }
            }
            return Content("修改失败");
        }

        /// <summary>
        /// 根据时间逆序分页显示留言板的内容
        /// </summary>
        /// <returns></returns>
        public ActionResult liuyanList(int page = 1)
        {
            using (WeiQingEntities db = new WeiQingEntities())
            {
                var q = db.liuyanban.OrderByDescending(x => x.state).ThenByDescending(x => x.addtime);
                var p = new EFPaging<liuyanban>();
                var list = p.getPageList(q, "/admin/liuyanList", page, 20);
                ViewData["list"] = list;
                ViewData["url"] = p.pageUrl;
            }
            return View();
        }

        /// <summary>
        /// 修改留言内容的状态
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="state">设定的状态值</param>
        /// <returns></returns>
        public ActionResult liuyanState(int id = 0, int state = -1)
        {
            if (id > 0 && state >= 0)
            {
                using (WeiQingEntities db = new WeiQingEntities())
                {
                    var m = db.liuyanban.Where(x => x.id == id).FirstOrDefault();
                    if (m != null && m.id > 0)
                    {
                        m.state = state;
                        return Content(db.SaveChanges().ToString());
                    }
                }
            }
            return Content("0");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult category()
        {
            ViewData["flList"] = EfExt.selectAll<category>();
            return View();
        }

        /// <summary>
        /// 添加文章分类
        /// </summary>
        /// <param name="model"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public ActionResult addCategory(category model, HttpPostedFileWrapper img)
        {
            if (model.category_name != null && model.category_name.Length > 0 && model.sort >= 0 && model.sort <= 100 && img != null && img.ContentLength > 0)
            {
                using (WeiQingEntities db = new WeiQingEntities())
                {
                    var count = db.category.Count();
                    if (count >= 7)
                    {
                        return Content("当前已经添加7个分类,暂不能继续添加");
                    }
                    model.img = ImagesTools.save(imgs: img);
                    db.category.Add(model);
                    return Content(db.SaveChanges().ToString());
                }
            }
            return Content("添加失败");
        }

        /// <summary>
        /// 更新文章分类
        /// </summary>
        /// <param name="model"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public ActionResult updateCategory(category model, HttpPostedFileWrapper img)
        {
            if (model.id > 0 && model.category_name != null && model.category_name.Length > 0 && model.sort >= 0 && model.sort <= 100)
            {
                reflectModel.setValues(model);  // 图片为空,清除null值
                using (WeiQingEntities db = new WeiQingEntities())
                {
                    var m = db.category.Where(x => x.id == model.id).FirstOrDefault();
                    if (m != null && m.id > 0)
                    {
                        m.category_name = model.category_name;
                        // 如果已经上传了图片,则先删除旧图片
                        if (img != null && img.ContentLength > 0)
                        {
                            if (m.img.Length > 0)
                            {
                                ImagesTools.delete(m.img);
                            }
                            m.img = ImagesTools.save(imgs: img);   // 图片保存到图片上传文件夹
                        }
                        m.sort = model.sort;
                        int r = db.SaveChanges();
                        if (r > 0)
                        {
                            return Content("1");
                        }
                    }
                }
            }
            return Content("修改失败");
        }

        /// <summary>
        /// 帖子管理, 显示帖子列表视图页
        /// </summary>
        /// <param name="key"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult tieziList(string key = "", int page = 1)
        {
            using(WeiQingEntities db=new WeiQingEntities())
            {
                if (key != null && key.Length > 0)
                {
                    var q = from t in db.title
                             join u in db.user on t.uid equals u.id
                             where t.art_title.Contains(key) || t.keywords.Contains(key)
                             orderby t.top descending, t.sort ascending, t.state descending, t.addtime descending
                             select new TitleUserExt()
                             {
                                 id = t.id,
                                 addtime = t.addtime,
                                 art_title = t.art_title,
                                 keywords = t.keywords,
                                 nick_name = u.nick_name,
                                 sort = t.sort,
                                 state = t.state,
                                 top = t.top,
                                 uid = t.uid
                             };
                    // var q = db.title.Where(x => x.art_title.Contains(key) || x.keywords.Contains(key)).OrderByDescending(x => x.top).ThenBy(x => x.sort).ThenBy(x => x.state).ThenByDescending(x => x.addtime);
                    var p = new EFPaging<TitleUserExt>();
                    var tlist = p.getPageList(q, "/admin/tieziList", page, 20);
                    ViewData["tlist"] = tlist;
                    ViewData["url"] = p.pageUrl;
                    return View();
                }
                else
                {
                    var q = from t in db.title
                            join u in db.user on t.uid equals u.id
                            orderby t.top descending, t.sort ascending, t.state descending, t.addtime descending
                            select new TitleUserExt()
                            {
                                id = t.id,
                                addtime = t.addtime,
                                art_title = t.art_title,
                                keywords = t.keywords,
                                nick_name = u.nick_name,
                                sort = t.sort,
                                state = t.state,
                                top = t.top,
                                uid = t.uid
                            };
                    // var q = db.title.OrderByDescending(x => x.top).ThenBy(x => x.sort).ThenBy(x => x.state).ThenByDescending(x => x.addtime);
                    var p = new EFPaging<TitleUserExt>();
                    var tlist = p.getPageList(q, "/admin/tieziList", page, 20);
                    ViewData["tlist"] = tlist;
                    ViewData["url"] = p.pageUrl;
                }
            }
            return View();
        }

        /// <summary>
        /// 管理员更新帖子的设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult editTitle(title model)
        {
            if (model != null && model.id > 0 && model.sort <= 100 && model.sort >= 0)
            {
                using(WeiQingEntities db=new WeiQingEntities())
                {
                    var m = db.title.Where(x => x.id == model.id).FirstOrDefault();
                    if (m != null && m.id > 0)
                    {
                        m.top = model.top;
                        m.sort = model.sort;
                        m.state = model.state;
                        return Content(db.SaveChanges().ToString());
                    }
                }
            }
            return Content("修改失败");
        }

    }
}