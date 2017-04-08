using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDao.Entity;

namespace EFDao.EntityExt
{
    /// <summary>
    /// 用户创建的帖子显示标题在首页
    /// </summary>
    public class TitleUserExt : title
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string nick_name { get; set; }
    }
}
