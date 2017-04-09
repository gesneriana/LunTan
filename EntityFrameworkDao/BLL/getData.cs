using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDao.Entity;
using EFDao.EntityExt;

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
            using(WeiQingEntities db=new WeiQingEntities())
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
            using(WeiQingEntities db=new WeiQingEntities())
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
            using(WeiQingEntities db=new WeiQingEntities())
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
            using (WeiQingEntities db = new WeiQingEntities())
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
    }
}
