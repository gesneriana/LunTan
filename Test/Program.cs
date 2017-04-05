using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Utility;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var user = new EFDao.Entity.user() { nick_name = "gesneriana", email = "s694060865@163.com", pwd = "angel." };
            reflectModel.setValues(user);
            using(EFDao.Entity.WeiQingEntities db=new EFDao.Entity.WeiQingEntities())
            {
                db.user.Add(user);
                db.SaveChanges();
            }
            */
            var t = typeof(EFDao.Entity.article);
            Console.WriteLine(t.Name);
        }
    }
}
