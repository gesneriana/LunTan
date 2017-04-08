using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDao.EntityExt
{
    /// <summary>
    /// 包含帖子内容和楼主用户名的实体,用于创建帖子的实体类
    /// </summary>
    public class TitleExt : EFDao.Entity.title
    {
        /// <summary>
        /// 一楼的内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 用户名,显示帖子详情页的时候,用于包装多表查询的对象
        /// </summary>
        public string uname { get; set; }
    }
}
