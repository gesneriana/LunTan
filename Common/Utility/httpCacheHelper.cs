using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Common.Utility
{
    /// <summary>  
    /// Cache辅助类, 取出缓存时应注意 数据是否过旧(更新了数据库), 还需要检查缓存是否已过期, 可能取出来null值
    /// </summary>  
    public class httpCacheHelper
    {
        /// <summary>  
        /// 获取数据缓存  
        /// </summary>  
        /// <param name="CacheKey">键</param>  
        public static object GetCache(string CacheKey)
        {
            Cache objCache = HttpRuntime.Cache;
            return objCache[CacheKey];
        }

        /// <summary>
        /// 获取指定类型的缓存对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="getDefaultValue">如果对象为null,获取类型的默认值</param>
        /// <returns></returns>
        public static T getCacheModel<T>(string cacheKey, bool getDefaultValue = true) where T : new()
        {
            Cache objCache = HttpRuntime.Cache;
            var m = objCache[cacheKey];
            if (m == null)
            {
                if (getDefaultValue)
                {
                    return new T();
                }
                else
                {
                    return default(T);
                }
            }
            return (T)m;
        }

        /// <summary>
        /// 获取指定类型的缓存泛型集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="getDefaultValue"></param>
        /// <returns></returns>
        public static List<T> getCacheList<T>(string cacheKey, bool getDefaultValue = true)
        {
            Cache objCache = HttpRuntime.Cache;
            var m = objCache[cacheKey];
            if (m == null)
            {
                if (getDefaultValue)
                {
                    return new List<T>();
                }
                else
                {
                    return null;
                }
            }
            return (List<T>)m;
        }

        /// <summary>
        /// 获取指定键值类型的字典集合
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="getDefaultValue"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> getCacheDictionary<TKey, TValue>(string cacheKey, bool getDefaultValue = true)
        {
            Cache objCache = HttpRuntime.Cache;
            var m = objCache[cacheKey];
            if (m == null)
            {
                if (getDefaultValue)
                {
                    return new Dictionary<TKey, TValue>();
                }
                else
                {
                    return null;
                }
            }
            return (Dictionary<TKey, TValue>)m;
        }

        /// <summary>
        /// 设置指定的分钟之后过期, 如果修改缓存在数据库中的数据, 则应立即清除缓存, 以免加载了旧数据
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="model"></param>
        /// <param name="minute"></param>
        public static void setCache(string CacheKey, object model, int minute = 30)
        {
            Cache cache = HttpRuntime.Cache;
            cache.Insert(CacheKey, model, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, minute, 0));
        }


        /// <summary>  
        /// 移除指定数据缓存  
        /// </summary>  
        public static void RemoveCacheByKey(string CacheKey)
        {
            Cache _cache = HttpRuntime.Cache;
            _cache.Remove(CacheKey);
        }

        /// <summary>  
        /// 移除全部缓存  
        /// </summary>  
        public static void RemoveAllCache()
        {
            Cache _cache = HttpRuntime.Cache;
            var CacheEnum = _cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                _cache.Remove(CacheEnum.Key.ToString());
            }
        }
    }
}
