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
    
    public partial class article
    {
        public long id { get; set; }
        public int cateid { get; set; }
        public string title { get; set; }
        public string keywords { get; set; }
        public System.DateTime addtime { get; set; }
        public int uid { get; set; }
        public int state { get; set; }
        public bool top { get; set; }
        public int sort { get; set; }
        public string content { get; set; }
        public string img { get; set; }
    }
}
