using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDao.EntityExt
{
    /// <summary>
    /// 继承文章分类实体类, 保存当前分类的指定排序规则的前10条文章
    /// </summary>
    public class CategoryAricleExt : Entity.category
    {
        /// <summary>
        /// 当前文章分类下的前10条文章
        /// </summary>
        public List<Entity.article> artlist { get; set; }
    }
}
