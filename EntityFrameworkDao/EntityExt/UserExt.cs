using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDao.EntityExt
{
    /// <summary>
    /// 用户类的扩展,用户注册
    /// </summary>
    public class UserExt : EFDao.Entity.user
    {
        /// <summary>
        /// 新密码
        /// </summary>
        public string newpwd { get; set; }

        /// <summary>
        /// 修改用户资料时,保存用户的旧密码,已经被sha1算法处理过
        /// </summary>
        public string oldpwd { get; set; }
    }
}
