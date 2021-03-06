﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDao.EntityExt
{
    /// <summary>
    /// 帖子的扩展,包含回复内容的列表
    /// </summary>
    public class TieZiExt : Entity.tiezi
    {
        /// <summary>
        /// 帖子的某个楼层的回复内容列表
        /// </summary>
        public List<Entity.tzreply> replyList { get; set; }
    }
}
