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
    /// 使用Entity Framework进行增删改, 进行封装, 查询没办法做到标准的通用
    /// </summary>
    public class EFCommon
    {
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
