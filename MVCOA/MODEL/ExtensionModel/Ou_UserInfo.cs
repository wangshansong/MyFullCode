using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    /// <summary>
    ///  扩展 用户 实体类
    /// </summary>
    public partial class Ou_UserInfo
    {
        public Ou_UserInfo ToPOCO()
        {
            Ou_UserInfo poco = new Ou_UserInfo()
            {
                uId = this.uId,
                uDepId = this.uDepId,
                uLoginName = this.uLoginName,
                uPwd = this.uPwd,
                uGender = this.uGender,
                uPost = this.uPost,
                uRemark = this.uRemark,
                uIsDel = this.uIsDel,
                uAddTime = this.uAddTime
            };
            return poco;
        }
    }
}
