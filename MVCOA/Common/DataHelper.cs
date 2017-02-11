using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Common
{
    /// <summary>
    /// 常用的数据帮助方法
    /// </summary>
    public static  class DataHelper
    {
        /// <summary>
        /// 序列化器对象
        /// </summary>
        static JavaScriptSerializer jss = new JavaScriptSerializer();

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns>加密后的密文</returns>
        public static string MD5(string str)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
        }

        /// <summary>
        /// 将对象进行序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjToJson(object obj)
        {
            return jss.Serialize(obj);
        }


    }
}
