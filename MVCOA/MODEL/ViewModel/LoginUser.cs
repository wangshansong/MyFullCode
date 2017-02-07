using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;//验证 的程序集

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
        [Required] //验证，必填项
        public string uLoginName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [Required]
        public string uPwd { get; set; }
        /// <summary>
        /// 是否记住用户名 
        /// </summary>
        public bool isAlways { get; set; }
    }
}
