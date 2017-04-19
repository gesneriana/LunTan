using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDao.Entity;
using EFDao.EntityExt;
using Common.Utility;

namespace EFDao.BLL
{
    /// <summary>
    /// 查询数据库,主要用于执行模板页的查询
    /// </summary>
    public class getData
    {
        /// <summary>
        /// 模板页获取show=true的公告
        /// </summary>
        /// <returns></returns>
        public static notice getNotice()
        {
            using(bazifengshuisuanmingEntities db=new bazifengshuisuanmingEntities())
            {
                var notice = db.notice.Where(x => x.show == true).OrderByDescending(x => x.addtime).FirstOrDefault();
                return notice;
            }
        }

        /// <summary>
        /// 模板页获取可以显示的文章列表,按时间倒序,只取前10条
        /// </summary>
        /// <returns></returns>
        [Obsolete("文章实体类已更新,排序规则需要更改")]
        public static List<article> getArtList()
        {
            using(bazifengshuisuanmingEntities db=new bazifengshuisuanmingEntities())
            {
                var list = db.article.Where(x => x.state == 1).OrderByDescending(x => x.addtime).Take(10).ToList();
                if (list == null)
                {
                    return new List<article>();
                }
                return list;
            }
        }

        /// <summary>
        /// PC版模板页获取最新的10条留言板内容
        /// </summary>
        /// <returns></returns>
        public static List<liuyanban> getLybList()
        {
            using(bazifengshuisuanmingEntities db=new bazifengshuisuanmingEntities())
            {
                return db.liuyanban.Where(x => x.state == 1).OrderByDescending(x => x.addtime).Take(10).ToList();
            }
        }

        /// <summary>
        /// 获取前10条帖子, 置顶->排序->状态(显示优先)->添加时间 逆序
        /// </summary>
        /// <returns></returns>
        public static List<TitleUserExt> getTitleList()
        {
            using (bazifengshuisuanmingEntities db = new bazifengshuisuanmingEntities())
            {
                var q = from t in db.title
                        join u in db.user on t.uid equals u.id
                        where t.state == 1
                        orderby t.top descending, t.sort ascending, t.addtime descending
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
                return q.Take(10).ToList();
            }
        }

        /// <summary>
        /// 模板页获取广告内容
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, adcontent> getAdContent()
        {
            var temp = httpCacheHelper.getCacheDictionary<string, adcontent>("adcontent_dic");
            if (temp != null && temp.Keys.Count > 0)
            {
                return temp;     // 读取缓存
            }

            var dic = new Dictionary<string, adcontent>();
            using (bazifengshuisuanmingEntities db = new bazifengshuisuanmingEntities())
            {
                var left = db.adcontent.Where(x => x.weizhi == 1).FirstOrDefault();
                if (left != null && left.id > 0)
                {
                    dic.Add("left", left);
                }

                var right = db.adcontent.Where(x => x.weizhi == 2).FirstOrDefault();
                if (right != null && right.id > 0)
                {
                    dic.Add("right", right);
                }

                var title = db.adcontent.Where(x => x.weizhi == 3).FirstOrDefault();
                if (title != null && title.id > 0)
                {
                    dic.Add("title", title);
                }

                var foot = db.adcontent.Where(x => x.weizhi == 4).FirstOrDefault();
                if (foot != null && foot.id > 0)
                {
                    dic.Add("foot", foot);
                }

                httpCacheHelper.setCache("adcontent_dic", dic, 10);
            }
            return dic;
        }

    }
}
