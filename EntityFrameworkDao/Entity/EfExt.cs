using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDao.Entity
{
    /// <summary>
    /// 使用Entity Framework进行增删改, 进行封装, 查询没办法做到通用, 
    /// 尽量使用泛型方法,小写字母开头的方法, 如果需要先进行查询然后同时更新数据,或者更改多张表的对象, 尽量不使用这个工具类, 因为会多次打开与数据库的连接
    /// 这时可以使用反射将扩展类复制为基类对象
    /// </summary>
    public class EfExt
    {

        /// <summary>
        /// 使用linq进行简单的泛型查询,一次最多只能得到一条记录
        /// </summary>
        /// <typeparam name="T">需要得到的类型, 查询哪张表(必须和表名称一致)</typeparam>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public static T select<T>(int id) where T : class, new()
        {
            string tableName = typeof(T).Name;
            using (WeiQingEntities db = new WeiQingEntities())
            {
                var set = db.Set<T>();
                var model = set.SqlQuery(string.Format("select * from `{0}` where id={1}", tableName, id)).FirstOrDefault();
                return model;
            }
        }

        /// <summary>
        /// 使用命名参数查询,尽量不要使用此方法, 可能会一次性查询出大量的数据, 应该使用分页查询通用方法
        /// SqlQuery(select * from article keywords like @p0, "%"+keywords+"%")
        /// </summary>
        /// <typeparam name="T">需要得到的类型, 查询哪张表(必须和表名称一致)</typeparam>
        /// <param name="sql">查询语句,参照注释</param>
        /// <param name="param">查询参数</param>
        /// <returns></returns>
        public static List<T> selectList<T>(string sql, params object[] param) where T : class, new()
        {
            using (WeiQingEntities db = new WeiQingEntities())
            {
                var set = db.Set<T>();
                var list = set.SqlQuery(sql, param).ToList();
                return list;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int Insert(object obj)
        {
            Type t = obj.GetType();
            int effect = -1;
            using (WeiQingEntities con = new WeiQingEntities())
            {
                con.Database.Log = new Action<string>(q => Debug.WriteLine(q));
                DbSet set = con.Set(t);
                set.Add(obj);
                effect = con.SaveChanges();
                return effect;
            }
        }

        /// <summary>
        /// 使用泛型保存对象到数据库中, 类型参数可以是 基类, 参数model对象可以是 扩展类对象
        /// </summary>
        /// <typeparam name="T">基类</typeparam>
        /// <param name="model">子类对象</param>
        /// <returns></returns>
        public static int insert<T>(T model) where T : class
        {
            using (WeiQingEntities db = new WeiQingEntities())
            {
                DbSet<T> set = db.Set<T>();
                set.Add(model);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public static int InsertMany(IEnumerable<object> objs)
        {
            int effect = 0;

            var et = objs.GetEnumerator();
            if (et.MoveNext())
            {
                Type t = et.Current.GetType();
                using (WeiQingEntities con = new WeiQingEntities())
                {
                    con.Database.Log = new Action<string>(q => Debug.WriteLine(q));
                    DbSet set = con.Set(t);
                    foreach (var o in objs)
                    {
                        set.Add(o);
                    }
                    effect = con.SaveChanges();
                }
            }
            return effect;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int Update(object obj)
        {
            int effect = -1;
            using (WeiQingEntities con = new WeiQingEntities())
            {
                con.Database.Log = new Action<string>(q => Debug.WriteLine(q));
                DbEntityEntry entry = con.Entry(obj);
                entry.State = EntityState.Modified;
                effect = con.SaveChanges();
                return effect;
            }
        }

        /// <summary>
        /// 使用泛型保存更改到数据库中, 类型参数可以是 基类, 参数model对象可以是 扩展类对象
        /// </summary>
        /// <typeparam name="T">基类</typeparam>
        /// <param name="model">可以是扩展类对象</param>
        /// <returns></returns>
        public static int update<T>(T model) where T : class
        {
            using (WeiQingEntities db = new WeiQingEntities())
            {
                var ent = db.Entry(model);
                ent.State = EntityState.Modified;
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public static int UpdateMany(IEnumerable<object> objs)
        {
            int effect = 0;
            var et = objs.GetEnumerator();
            if (et.MoveNext())
            {
                Type t = et.Current.GetType();
                using (WeiQingEntities con = new WeiQingEntities())
                {
                    con.Database.Log = new Action<string>(q => Debug.WriteLine(q));
                    foreach (var o in objs)
                    {
                        DbEntityEntry entry = con.Entry(o);
                        entry.State = EntityState.Modified;
                    }
                    effect = con.SaveChanges();
                }
            }

            return effect;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int Delete(object obj)
        {
            int effect = -1;
            using (WeiQingEntities con = new WeiQingEntities())
            {
                con.Database.Log = new Action<string>(q => Debug.WriteLine(q));
                DbEntityEntry entry = con.Entry(obj);
                entry.State = EntityState.Deleted;
                effect = con.SaveChanges();
                return effect;
            }
        }

        /// <summary>
        /// 使用泛型删除数据库中的对象, 类型参数可以是 基类, 参数model对象可以是 扩展类对象
        /// </summary>
        /// <typeparam name="T">基类</typeparam>
        /// <param name="model">可以是扩展类对象</param>
        /// <returns></returns>
        public static int delete<T>(T model) where T : class
        {
            using(WeiQingEntities db=new WeiQingEntities())
            {
                var ent = db.Entry(model);
                ent.State = EntityState.Deleted;
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public static int DeleteMany(IEnumerable<object> objs)
        {
            int effect = 0;

            var et = objs.GetEnumerator();
            if (et.MoveNext())
            {
                Type t = et.Current.GetType();
                using (WeiQingEntities con = new WeiQingEntities())
                {
                    con.Database.Log = new Action<string>(q => Debug.WriteLine(q));
                    foreach (var o in objs)
                    {
                        DbEntityEntry entry = con.Entry(o);
                        entry.State = EntityState.Deleted;
                    }
                    effect = con.SaveChanges();
                }
            }
            return effect;
        }
    }
}
