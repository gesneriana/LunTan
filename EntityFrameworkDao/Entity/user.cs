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
    
    public partial class user
    {
        public long id { get; set; }
        public string nick_name { get; set; }
        public string pwd { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string qq { get; set; }
        public string wei_xin { get; set; }
        public System.DateTime reg_date { get; set; }
        public bool is_admin { get; set; }
        public int state { get; set; }
    }
}
