using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public partial  interface IOu_UserInfoBLL
    {
        /// <summary>
        /// 登录验证方法
        /// </summary>
        /// <param name="strName">用户名</param>
        /// <param name="strPwd">密码</param>
        MODEL.Ou_UserInfo Login(string strName, string strPwd);

    
    }
}
