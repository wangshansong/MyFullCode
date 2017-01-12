using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVCOA.Helper;

namespace MVCOA.Logic
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IBLL.IOu_PermissionBLL iOu_Ou_Permission = OperateContext.BLLSession.IOu_PermissionBLL;

            List<MODEL.Ou_Permission> tempList = iOu_Ou_Permission.GetListBy(p => p.pIsDel == false);
            return View(tempList);
        }
    }
}
