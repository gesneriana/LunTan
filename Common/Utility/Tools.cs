using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Utility
{
    /// <summary>
    /// 常用的方法
    /// </summary>
    public class Tools
    {
        /// <summary>
        /// 邮件服务器主机地址
        /// </summary>
        private static string emailHostAddress = ConfigurationManager.AppSettings["emailHostAddress"];

        /// <summary>
        /// 邮箱账号,用于登录邮件服务器
        /// </summary>
        private static string emailUserName = ConfigurationManager.AppSettings["emailUserName"];

        /// <summary>
        /// 邮箱账号的授权码,用于登录邮件服务器
        /// </summary>
        private static string emailUserPassCode = ConfigurationManager.AppSettings["emailUserPassCode"];

        /// <summary>
        /// 邮件的发件人地址
        /// </summary>
        private static string emailFromAddress= ConfigurationManager.AppSettings["emailFromAddress"];

        /// <summary>
        /// 显示的发件人名称
        /// </summary>
        private static string emailFromName = ConfigurationManager.AppSettings["emailFromName"];

        /// <summary>
        /// 获取字符串的长度,汉字占两个字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int getStrLength(string str)
        {
            return Encoding.UTF8.GetByteCount(str);
        }

        /// <summary>
        /// 验证邮箱格式是否正确,正确的格式为 true
        /// </summary>
        /// <param name="email">电子邮箱字符串</param>
        /// <returns>返回值为 true 通过验证</returns>
        public static bool IsEmail(string email)
        {
            Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");

            if (r.IsMatch(email))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 发送邮件,两个必须参数,收件人邮箱和邮件内容
        /// </summary>
        /// <param name="email">收件人邮箱</param>
        /// <param name="str">邮件内容</param>
        /// <param name="title">邮件主题</param>
        /// <param name="fromUserName">发件人显示的姓名或者名称</param>
        /// <returns>发送成功,或者是 错误信息</returns>
        public static string SendEmail(string email, string str, string title = "找回密码",string fromUserName= "魏清八字风水算命论坛")
        {
            try
            {
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.Host = emailHostAddress;     //使用163的SMTP服务器发送邮件
                client.UseDefaultCredentials = true;
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                client.Credentials = new System.Net.NetworkCredential(emailUserName, emailUserPassCode);    // 账号和授权码
                //163的SMTP服务器需要用163邮箱的用户名和授权码作认证，如果没有需要去163申请个,                                                                         
                //这里假定你已经拥有了一个163邮箱的账户，用户名为abc，授权码为******* 

                System.Net.Mail.MailMessage Message = new System.Net.Mail.MailMessage();

                if (emailFromName != null && "魏清八字风水算命论坛".Equals(fromUserName))
                {
                    Message.From = new System.Net.Mail.MailAddress(emailFromAddress, emailFromName, Encoding.UTF8); // 如果参数为默认值,则读取配置文件
                }
                else
                {
                    Message.From = new System.Net.Mail.MailAddress(emailFromAddress, fromUserName, Encoding.UTF8);  // 提供了参数,优先使用参数值
                }
                //这里需要注意，163似乎有规定发信人的邮箱地址必须是163的，而且发信人的邮箱用户名必须和上面SMTP服务器认证时的用户名相同                                                               
                //因为上面用的用户名abc作SMTP服务器认证，所以这里发信人的邮箱地址也应该写为abc@163.com

                Message.To.Add(email);
                //将邮件发送给Gmail
                //Message.To.Add("123456@qq.com");//将邮件发送给QQ邮箱

                Message.Subject = title;
                Message.Body = str;
                Message.SubjectEncoding = Encoding.UTF8;
                Message.BodyEncoding = Encoding.UTF8;
                Message.Priority = System.Net.Mail.MailPriority.High;
                Message.IsBodyHtml = true;
                client.Send(Message);
                return "发送成功";
            }
            catch (Exception ex)
            {
                return ex.Message;  // 邮件发送失败
            }
        }

        /// <summary>
        /// 获取一个随机字符串
        /// </summary>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string getRandomStr(int len=8)
        {
            string s = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm7410852963.";
            StringBuilder sb = new StringBuilder();
            Random r = new Random();
            for (int i = 0; i < len; i++)
            {
                sb.Append(s[r.Next(s.Length)]);
            }
            return sb.ToString();
        }


        /// <summary>  
        /// 获取客户端的真正ip  
        /// </summary>  
        /// <returns></returns>  
        public static string GetRealIP()
        {
            string result = String.Empty;
            result = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            //可能有代理   
            if (!string.IsNullOrWhiteSpace(result))
            {
                //没有"." 肯定是非IP格式  
                if (result.IndexOf(".") == -1)
                {
                    result = null;
                }
                else
                {
                    //有","，估计多个代理。取第一个不是内网的IP。  
                    if (result.IndexOf(",") != -1)
                    {
                        result = result.Replace(" ", string.Empty).Replace("\"", string.Empty);

                        string[] temparyip = result.Split(",;".ToCharArray());

                        if (temparyip != null && temparyip.Length > 0)
                        {
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                //找到不是内网的地址  
                                if (IsIPAddress(temparyip[i]) && temparyip[i].Substring(0, 3) != "10." && temparyip[i].Substring(0, 7) != "192.168" && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];
                                }
                            }
                        }
                    }
                    //代理即是IP格式  
                    else if (IsIPAddress(result))
                    {
                        return result;
                    }
                    //代理中的内容非IP  
                    else
                    {
                        result = null;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                result = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                result = System.Web.HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }

        /// <summary>
        /// 判断是否为ip地址
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static bool IsIPAddress(string str)
        {
            if (string.IsNullOrWhiteSpace(str) || str.Length < 7 || str.Length > 15)
                return false;

            string regformat = @"^(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})";
            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);

            return regex.IsMatch(str);
        }
    }
}
