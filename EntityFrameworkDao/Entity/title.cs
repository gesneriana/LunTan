//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFDao.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class title
    {
        public long id { get; set; }
        public long uid { get; set; }
        public string art_title { get; set; }
        public string keywords { get; set; }
        public bool top { get; set; }
        public int sort { get; set; }
        public long state { get; set; }
        public System.DateTime addtime { get; set; }
    }
}
