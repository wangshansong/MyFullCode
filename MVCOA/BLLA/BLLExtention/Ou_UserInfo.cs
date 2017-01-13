using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLA
{
    public partial class Ou_UserInfo:IBLL.IOu_UserInfoBLL
    {
        /// <summary>
        /// 登录验证方法
        /// </summary>
        /// <param name="strName">用户名</param>
        /// <param name="strPwd">密码</param>
        /// <returns></returns>
        public MODEL.Ou_UserInfo Login(string strName, string strPwd)
        {
            MODEL.Ou_UserInfo user = base.GetListBy(p=>p.uLoginName == strName).FirstOrDefault();
            if (user != null && user.uPwd == DataHelper.MD5(strPwd))
            {
                return user.ToPOCO();
            }
            return null;
        }
    }
}
