using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;

namespace Common.Utility
{
    /// <summary>
    /// 分页
    /// </summary>
    /// <typeparam name="TSource">实体类的类型名称</typeparam>
    public class EFPaging<TSource>
    {
        /// <summary>
        /// 分页的html页面内容
        /// </summary>
        public string pageUrl { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int total_count { get; set; }

        /// <summary>
        /// 得到分页数据,可以不立即返回集合,返回 IQueryable<TSource> 延迟加载
        /// 注意; Select()只有在Take()后面才能起到作用, 如果某一列包含大量数据
        /// 而且不需要立即显示出来, 建议使用重载方法, 加上Select()选择列
        /// </summary>
        /// <param name="q">查询条件</param>
        /// /// <param name="url">需要分页的请求路径,可以带上参数</param>
        /// <param name="page">页码, >= 1</param>
        /// <param name="count">每页显示的数量,5-20之间</param>
        /// /// <param name="btnCount">显示的可以点击的按钮的个数 3-10之间</param>
        /// <returns>List泛型集合</returns>
        public List<TSource> getPageList(IQueryable<TSource> q, string url, int page = 1, int count = 10, int btnCount = 5)
        {
            #region 计算总记录数以及页码等逻辑
            if (count < 5) { count = 5; } else if (count > 20) { count = 20; }
            if (btnCount < 3) { btnCount = 3; } else if (btnCount > 20) { btnCount = 20; }
            int totalCount = q.Count(); // 总记录行数
            total_count = totalCount;
            int totalPage = 0;  // 总页数
            if (totalCount > 0)
            {
                totalPage = totalCount / count;
                if (totalCount % count > 0)
                {
                    totalPage++;
                }
            }
            if (page >= totalPage) { page = totalPage; } else if (page < 1) { page = 1; }   // 页码超过最大值,更改为最大的页码
            #endregion

            if (totalCount == 0)
            {
                pageUrl = string.Empty;
                return new List<TSource>(); // 无数据
            }
            if (totalCount <= count)
            {
                pageUrl = string.Empty;
                return q.ToList();  // 数量小于或者等于1页
            }
            if (page > 1)
            {
                pageUrl = getPageHref(totalCount, url, page, count, btnCount);
                return q.Skip((page - 1) * count).Take(count).ToList();    // 跳过前面的数据,获取指定页码的count条数据
            }
            pageUrl = getPageHref(totalCount, url, page, count, btnCount);
            return q.Take(count).ToList();
        }

        /// <summary>
        /// 使用entity framework框架进行自动分页, 如果需要使用ajax无刷新加载, 可以修改 a 链接的href='javascript:;' onclick='getPageInfo()'
        /// </summary>
        /// <param name="totalcount">总记录行数</param>
        /// <param name="url">a链接href属性的值,可以带上参数</param>
        /// <param name="page">页码</param>
        /// <param name="count">每页显示的数量</param>
        /// <param name="btnCount">翻页按钮的个数</param>
        /// <returns>翻页的a链接按钮</returns>
        private string getPageHref(int totalcount, string url, int page = 1, int count = 10, int btnCount = 5)
        {
            #region 计算页数
            int totalPage = 0;  // 总页数
            if (totalcount > 0)
            {
                totalPage = totalcount / count;
                if (totalcount % count > 0)
                {
                    totalPage++;
                }
            }
            if (totalPage <= 1)
            {
                return "";
            }
            #endregion

            StringBuilder sb = new StringBuilder();
            string s = string.Empty;    // 保存临时拼接的字符串的数据
            sb.Append("<div class='badoo'>");
            #region 上一页
            if (page < 2)
            {
                sb.Append("<span class='disabled'>上一页</span>");
            }
            else
            {
                int temp = page - 1;
                if (url.IndexOf('?') > 0)
                {
                    s += "&page=" + temp;
                }
                else
                {
                    s += "?page=" + temp;
                }
                sb.AppendFormat("<a href='{0}'>上一页</a>", url + s);
            }
            #endregion

            #region 中间的分页
            if (page <= (btnCount / 2))
            {
                for (int p = 1; p <= btnCount && p <= totalPage; p++)
                {
                    if (p == page)
                    {
                        sb.AppendFormat("<span class='current'>{0}</span>", p);
                    }
                    else
                    {
                        s = "";
                        if (url.IndexOf('?') > 0)
                        {
                            s += "&page=" + p;
                        }
                        else
                        {
                            s += "?page=" + p;
                        }
                        sb.AppendFormat("<a href='{0}'>{1}</a>", url + s, p);
                    }
                }
            }
            else
            {
                for (int p1 = page - (btnCount / 2); p1 < page; p1++)
                {
                    s = "";
                    if (url.IndexOf('?') > 0)
                    {
                        s += "&page=" + p1;
                    }
                    else
                    {
                        s += "?page=" + p1;
                    }
                    sb.AppendFormat("<a href='{0}'>{1}</a>", url + s, p1);
                }
                sb.AppendFormat("<span class='current'>{0}</span>", page);
                for (int p2 = page + 1; p2 < page + (btnCount / 2) && p2 <= totalPage; p2++)
                {
                    s = "";
                    if (url.IndexOf('?') > 0)
                    {
                        s += "&page=" + p2;
                    }
                    else
                    {
                        s += "?page=" + p2;
                    }
                    sb.AppendFormat("<a href='{0}'>{1}</a>", url + s, p2);
                }
            }
            #endregion

            #region 下一页
            if (page >= totalPage)
            {
                sb.AppendFormat("<sapn class='disabled'>下一页</span>");
            }
            else
            {
                int tpage1 = page + 1;
                string temp = "";
                if (url.IndexOf('?') > 0)
                {
                    temp += "&page=" + tpage1;
                }
                else
                {
                    temp += "?page=" + tpage1;
                }
                sb.AppendFormat("<a href='{0}'>下一页</a>", url + temp);
            }
            #endregion
            sb.Append("</div>");
            return sb.ToString();
        }
    }
}