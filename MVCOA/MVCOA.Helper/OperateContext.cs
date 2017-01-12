using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBLL;
using DI;
namespace MVCOA.Helper
{
    /// <summary>
    /// 通过容器生成业务层仓储
    /// </summary>
    public static class OperateContext
    {
       public static  IBLLSession BLLSession = DI.SpringHelper.GetObject<IBLLSession>("BLLSession");
    }
}
