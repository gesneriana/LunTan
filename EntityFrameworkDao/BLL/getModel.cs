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
    public class getModel
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
    }
}
