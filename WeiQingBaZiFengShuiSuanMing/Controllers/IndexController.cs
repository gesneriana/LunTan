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
    public class IndexController : Controller
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [Filters.IsMobile]
        public ActionResult index()
        {
            return View(ViewData["act"].ToString());
        }

        /// <summary>
        /// ajax加载电脑版的用户注册视图页
        /// </summary>
        /// <returns></returns>
        public ActionResult regView()
        {
            return View();
        }

        /// <summary>
        /// 用户注册的请求方法,注册成功,将用户保存到Session中
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        [Filters.CheckLogin(IsLogin = false)]
        public ActionResult regUser(user u)
        {
            if (u != null)
            {
                SetNullString.setValues(u);
                if (Tools.getStrLength(u.nick_name) < 3)
                    return Content("用户名的长度必须大于3个字符");
                if (u.pwd.Length < 6)
                    return Content("密码必须大于6个字符");
                if (!Tools.IsEmail(u.email))
                    return Content("邮箱格式不正确");

                u.pwd = HashTools.SHA1_Hash(u.pwd);
                DateTime dt = DateTime.Now;
                u.reg_date = dt;
                u.last_login_date = dt;

                try
                {
                    using (WeiQingEntities db = new WeiQingEntities())
                    {
                        var count = db.user.Where(p => p.nick_name.Equals(u.nick_name) || p.email.Equals(u.email)).Count();
                        if (count > 0)
                        {
                            return Content("此用户名或者邮箱已被注册");
                        }
                        u.is_admin = false;
                        db.user.Add(u);
                        int res = db.SaveChanges();
                        if (res > 0)
                        {
                            Session["user"] = u;
                        }
                        return Content(res.ToString());
                    }
                }
                catch (Exception ex)
                {
                    return Content("后台出现错误");
                }

            }
            return Content("没有获取到数据");
        }

        /// <summary>
        /// 用户登录,登录成功更新用户最后登录的时间,保存到session
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        [Filters.CheckLogin(IsLogin = false)]
        public ActionResult login(user u)
        {
            if (u != null)
            {
                SetNullString.setValues(u);
                if (u.nick_name.Length >= 3 && u.pwd.Length >= 6)
                {
                    u.pwd = HashTools.SHA1_Hash(u.pwd);

                    try
                    {
                        using (WeiQingEntities db = new WeiQingEntities())
                        {
                            var user = db.user.Where(p => (p.nick_name.Equals(u.nick_name) || p.email.Equals(u.nick_name))
                            && p.pwd.Equals(u.pwd)).FirstOrDefault();
                            if (user != null && (user.nick_name.Equals(u.nick_name) || user.email.Equals(u.nick_name)))
                            {
                                string ip = Tools.GetRealIP();
                                login_log log = new login_log() { uid = (int)user.id, login_ip = ip, login_time = DateTime.Now };
                                db.login_log.Add(log);
                                db.SaveChanges();
                                Session["user"] = user;
                                return Content("1");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return Content("后台出现错误:" + ex.Message);
                    }

                }
                return Content("用户名和密码不正确");
            }
            return Content("参数错误");
        }

        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns></returns>
        public ActionResult logout()
        {
            Session["user"] = null;
            return Redirect("/index/index");
        }

        /// <summary>
        /// 找回密码的视图页
        /// </summary>
        /// <returns></returns>
        public ActionResult forgetPwd()
        {
            return View();
        }

        /// <summary>
        /// 发送邮件,找回密码
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        [Filters.CheckLogin(IsLogin = false)]
        public ActionResult getPwd(user u)
        {
            if (u != null)
            {
                SetNullString.setValues(u);
                if (u.nick_name.Length >= 3 && Tools.IsEmail(u.email))
                {
                    try
                    {
                        using (WeiQingEntities db = new WeiQingEntities())
                        {
                            var user = db.user.Where(p => p.nick_name.Equals(u.nick_name) && p.email.Equals(u.email)).FirstOrDefault();
                            // 检查用户名和邮箱是否匹配
                            if (user != null && u.nick_name.Equals(user.nick_name))
                            {
                                DateTime dt = DateTime.Now;
                                string ip = Tools.GetRealIP();  // 获取客户端ip

                                // 检查当前 uid 一周之内是否已经找回过密码, 同一个ip一天之内之内找回3次密码
                                var t1 = dt.AddDays(-7);
                                var gpl_uid = db.getpwdlog.Where(p => p.uid == user.id && p.log_time > t1).Count();
                                if (gpl_uid > 0)
                                {
                                    return Content("一个星期之内只能找回一次密码");
                                }
                                var t2 = dt.AddHours(-24);
                                var gpl_ip = db.getpwdlog.Where(p => p.ip_address.Equals(ip) && p.log_time > t2).Count();
                                if (gpl_ip >= 3)
                                {
                                    return Content("同一个ip地址一天之内只能找回3次密码");
                                }

                                string newpwd = Tools.getRandomStr();
                                string res = Tools.SendEmail(u.email, "您的密码是:" + newpwd);   // 失败返回错误信息
                                if ("发送成功".Equals(res))
                                {
                                    var chPwdLog = new getpwdlog() { uid = (Int32)user.id, ip_address = ip, nick_name = user.nick_name, log_time = dt };
                                    db.getpwdlog.Add(chPwdLog); // 修改密码的日志
                                    user.pwd = HashTools.SHA1_Hash(newpwd);
                                    db.SaveChanges();   // 修改密码
                                }
                                return Content(res);
                            }
                            return Content("用户名或者邮箱不正确");
                        }
                    }
                    catch (Exception ex)
                    {
                        return Content("后台出现错误:" + ex.Message);
                    }
                }
            }
            return Content("参数错误");
        }

        /// <summary>
        /// 修改密码的视图页
        /// </summary>
        /// <returns></returns>
        public ActionResult changePwd()
        {
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        [Filters.CheckLogin]
        public ActionResult updatePwd(UserExt u)
        {
            if (u != null)
            {
                SetNullString.setValues(u);
                if (u.nick_name.Length >= 3 && u.pwd.Length >= 6 && u.newpwd.Length >= 6 && !u.pwd.Equals(u.newpwd))
                {
                    try
                    {
                        string old_hash_pwd = HashTools.SHA1_Hash(u.pwd);   // 旧的hash密码
                        using (WeiQingEntities db = new WeiQingEntities())
                        {
                            var user = db.user.Where(p => p.nick_name.Equals(u.nick_name) && p.pwd.Equals(old_hash_pwd)).FirstOrDefault();
                            if (user != null && u.nick_name.Equals(user.nick_name))
                            {
                                user.pwd = HashTools.SHA1_Hash(u.newpwd);
                                int res = db.SaveChanges();   // 修改密码
                                if (res > 0) { Session["user"] = null; }
                                return Content(res.ToString());
                            }
                            return Content("旧密码不正确");
                        }
                    }
                    catch (Exception ex)
                    {
                        return Content("后台出现错误");
                    }
                }
                return Content("修改失败");
            }
            return Content("参数错误");
        }

        /// <summary>
        /// 电脑版的八字预测说明页
        /// </summary>
        /// <returns></returns>
        public ActionResult BaZiYuCeExplain()
        {
            return View();
        }

    }
}