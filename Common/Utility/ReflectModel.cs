using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utility
{
    /// <summary>
    /// 将string类型值为null的属性初始化为 string.Empty, 将时间设置为当前时间
    /// 用反射从子类和父类之间相互复制属性值
    /// </summary>
    public class reflectModel
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
        /// <returns>返回基类对象</returns>
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

        /// <summary>
        /// 自动将基类对象复制到扩展继承类中,左侧为父类(基类),右侧为子类(扩展类), 参数为父类(基类)对象,子类(扩展类)未赋值的属性默认为null
        /// </summary>
        /// <typeparam name="TParent">父类(基类)</typeparam>
        /// <typeparam name="TChild">扩展业务逻辑继承的子类</typeparam>
        /// <param name="parent">基类对象</param>
        /// <returns>返回扩展的子类</returns>
        public static TChild AutoCopyToChild<TParent, TChild>(TParent parent) where TChild : new()
        {
            TChild ext_model = new TChild();
            var ParentType = typeof(TParent);
            var parentProp = ParentType.GetProperties();
            foreach (var Propertie in parentProp)
            {
                //循环遍历属性
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    //进行属性拷贝
                    Propertie.SetValue(ext_model, Propertie.GetValue(parent, null), null);
                }
            }
            return ext_model;
        }

    }
}
