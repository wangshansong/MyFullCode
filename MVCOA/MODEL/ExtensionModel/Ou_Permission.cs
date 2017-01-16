using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    /// <summary>
    /// 扩展 Ou_Permission
    /// </summary>
    public partial class Ou_Permission
    {
        public Ou_Permission ToPOCO()
        {
            Ou_Permission poco = new Ou_Permission()
            {
                pid = this.pid,
                pParent = this.pParent,
                pName = this.pName,
                pAreaName = this.pAreaName,
                pControllerName = this.pControllerName,
                pActionName = this.pActionName,
                pFormMethod = this.pFormMethod,
                pOperationType = this.pOperationType,
                pIco = this.pIco,
                pOrder = this.pOrder,
                pIsLink = this.pIsLink,
                pLinkUrl = this.pLinkUrl,
                pIsShow = this.pIsShow,
                pRemark = this.pRemark,
                pIsDel = this.pIsDel,
                pAddTime = this.pAddTime
            };

            return poco;
        }
    }
}
