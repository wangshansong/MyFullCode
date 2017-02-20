using MODEL.EasyUIModel;
using MVCOA.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVCOA.Logic.Admin
{
    public class SysController : Controller
    {
        [HttpGet]
        public ActionResult Permission()
        {
            return View();
        }

        public ActionResult GetPermData()
        {
            var pList = OperateContext.Current.BLLSession.IOu_PermissionBLL.GetListBy(p => p.pIsShow == true && p.pIsDel == false).Select(p=>p.ToPOCO());

            DataGridModel datagridModel = new DataGridModel()
            {
                total = (pList == null ? 0 : pList.Count()),
                rows = pList,
                footer = null
            };

            return Json(datagridModel);
        }

    [HttpPost]
        public ActionResult Editpermission(int ID)
        {
            var pList = OperateContext.Current.BLLSession.IOu_PermissionBLL.GetListBy(p =>p.pid == ID).Select(p => p.ToPOCO());
            
            return null;
        }
        
    }
}
