using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utility
{
    /// <summary>
    /// 将string类型值为null的属性初始化为 string.Empty
    /// </summary>
    public class ReflectModel
    {
        /// <summary>
        /// 将可空类型设置为 默认值 "", 日期时间则设置为DateTime.Now
        /// </summary>
        /// <param name="model">需要格式化默认值的对象</param>
        /// <param name="setDateTime">如果时间为 0001-01-01 00:00:00 则修改为当前时间</param>
        public static void setValues(object model, bool setDateTime = true)
        {
            Type t = model.GetType();
            PropertyInfo[] propertyInfo = t.GetProperties();

            if (setDateTime)
            {
                DateTime dt = DateTime.Now; // 当前时间戳
                foreach (var item in propertyInfo)
                {
                    if (item.PropertyType.ToString().Equals("System.String"))
                    {
                        if (item.CanWrite && item.GetValue(model, null) == null)
                            item.SetValue(model, string.Empty, null);
                    }
                    if (item.PropertyType.ToString().Equals("System.DateTime"))
                    {
                        if (item.CanWrite && (DateTime)item.GetValue(model, null) == DateTime.MinValue)
                            item.SetValue(model, dt, null);
                    }
                }
            }
            else
            {
                foreach (var item in propertyInfo)
                {
                    if (item.PropertyType.ToString().Equals("System.String"))
                    {
                        if (item.CanWrite && item.GetValue(model, null) == null)
                            item.SetValue(model, string.Empty, null);
                    }
                }
            }
        }

        /// <summary>
        /// 自动将子类对象复制到父类中,左侧为父类,右侧为子类, 参数为子类对象
        /// </summary>
        /// <typeparam name="TParent">父类</typeparam>
        /// <typeparam name="TChild">扩展业务逻辑继承的子类</typeparam>
        /// <param name="child">子类对象</param>
        /// <returns></returns>
        public static TParent AutoCopyToBase<TParent, TChild>(TChild child) where TParent : new()
        {
            TParent base_model = new TParent();
            var ParentType = typeof(TParent);
            var Properties = ParentType.GetProperties();
            foreach (var Propertie in Properties)
            {
                //循环遍历属性
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    //进行属性拷贝
                    Propertie.SetValue(base_model, Propertie.GetValue(child, null), null);
                }
            }
            return base_model;
        }
    }
}
