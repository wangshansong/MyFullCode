using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Common
{
    /// <summary>
    /// 安全助手类
    /// </summary>
    public static class SecurityHelper
    {
        /// <summary>
        /// 使用 票据对象 将 用户数据 加密成字符串
        /// </summary>
        /// <param name="userInfo">要加密的用户信息</param>
        /// <returns>密文</returns>
        public static  string EncryptUserInfo(string userInfo)
        {
            //1.1将要加密的数据存储票据对象中
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "userInfo", DateTime.Now, DateTime.Now, true, userInfo);
            //1.2 将票据对象 加密成字符串（可逆）
            return FormsAuthentication.Encrypt(ticket);
        }
        /// <summary>
        /// 用户数据解密
        /// </summary>
        /// <param name="cryptograph">通过票据对象加密的数据</param>
        /// <returns>解密后的数据</returns>
        public static string DecryptUserInfo( string cryptograph)
        {
            //1.1将加密的用户密文还原成票据对象
            FormsAuthenticationTicket ticket =  FormsAuthentication.Decrypt(cryptograph);
            //1.2 将票据里的 用户数据 返回
            return ticket.UserData;
        }
    }
}
