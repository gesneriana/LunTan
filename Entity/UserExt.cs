using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
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
    }
}
