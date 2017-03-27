using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class TitleExt : EFDao.Entity.title
    {
        /// <summary>
        /// 一楼的内容
        /// </summary>
        public string content { get; set; }
    }
}