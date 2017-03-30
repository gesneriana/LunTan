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
    public class SetNullString
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
    }
}
