using Common;
using MODEL.EasyUIModel;
using MODEL.FormatModel;
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
            var pList = OperateContext.Current.BLLSession.IOu_PermissionBLL.GetListBy(p => p.pIsShow == true && p.pIsDel == false).Select(p => p.ToPOCO());

            DataGridModel datagridModel = new DataGridModel()
            {
                total = (pList == null ? 0 : pList.Count()),
                rows = pList,
                footer = null
            };

            return Json(datagridModel);
        }

        [HttpGet]
        /// <summary>
        /// 加载 权限修改 窗体html
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult EditPermission(int ID)
        {
            // 根据id查询要修改的权限
            MODEL.ViewModel.Permission viewPermission = OperateContext.Current.BLLSession.IOu_PermissionBLL.GetListBy(p => p.pid == ID).FirstOrDefault().ToViewModel();
            //准备请求方式下拉框数据
            ViewBag.httpMethodList = new List<SelectListItem>(){
              new SelectListItem(){ Text="Get", Value="1" },
              new SelectListItem(){Text="Post",Value="2"}
            };
            /*
             * 准备操作方式方式下拉框数据
                0-无操作 1-easyui连接 2-打开新窗体
             */
            ViewBag.operationList = new List<SelectListItem>(){
              new SelectListItem(){ Text="无操作", Value="0" },
              new SelectListItem(){Text="easyui连接",Value="1"},
                new SelectListItem(){Text="打开新窗体",Value="2"}
            };
            return PartialView(viewPermission);
        }


        #region 1.2 权限修改 +EditPermission(MODEL.ViewModel.Permission model)
        [HttpPost]
        /// <summary>
        /// 1.2 权限修改
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPermission(MODEL.Ou_Permission model)
        {
            int res = OperateContext.Current.BLLSession.IOu_PermissionBLL.Modify(model, "pName", "pAreaName", "pControllerName", "pActionName", "pFormMethod", "pOperationType", "pOrder", "pIsShow", "pRemark");

            AjaxMsgModel msgOK = new AjaxMsgModel()
             {
                 Msg = "保存成功！",
                 Statu = "ok",
                 BackUrl = "/admin/sys/Permission",
             };

            AjaxMsgModel msgError = new AjaxMsgModel()
             {
                 Msg = "保存成功！",
                 Statu = "error",
                 BackUrl = "/admin/sys/Permission",
             };

            if (res > 0)
               return   Content(JsonHelper<AjaxMsgModel>.ModelToJsonString(msgOK));
            else
              return    Content(JsonHelper<AjaxMsgModel>.ModelToJsonString(msgError));
        }
        #endregion

        public ActionResult DeletePermission(int ID)
        {
            // 根据id查询要修改的权限
            int res = OperateContext.Current.BLLSession.IOu_PermissionBLL.DelBy(p=>p.pid == ID);
            if (res>0)
            {
      
            }
            return null;
        
        }
    }
}
