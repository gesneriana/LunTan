using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDao.Entity;

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
    }
}
