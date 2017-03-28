using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    /// <summary>
    /// 帖子的扩展,包含回复内容的列表
    /// </summary>
    public class TieZiExt : EFDao.Entity.tiezi
    {
        /// <summary>
        /// 帖子的某个楼层的回复内容列表
        /// </summary>
        public List<EFDao.Entity.tzreply> replyList { get; set; }
    }
}
