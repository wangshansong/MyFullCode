using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.ViewModel
{
    /// <summary>
    /// 用户登录模型
    /// </summary>
    public class LoginUser
    {
        /// <summary>
        /// 登录用户命名
        /// </summary>
        public string uLoginName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string uPwd { get; set; }
        /// <summary>
        /// 是否记住用户名 
        /// </summary>
        public bool isalways { get; set; }
    }
}
