using Common;
using MODEL;
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

        /// <summary>
        /// 查询数据加载到前台列表中
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPermData()
        {
            int pageSize = 0;
            int pageIndex = 1;
            int.TryParse(Request.Params["rows"], out pageSize); //页容量
            int.TryParse(Request.Params["page"], out pageIndex);//页码
            int rowCount = OperateContext.Current.BLLSession.IOu_PermissionBLL
                .GetListBy(p => p.pIsShow == true && p.pIsDel == false).Count();
            var pList = OperateContext.Current.BLLSession.IOu_PermissionBLL.
                GetPagedList(pageIndex, pageSize, p => p.pIsShow == true && p.pIsDel == false, p => p.pid).ToList().Select(p => p.ToPOCO());
            DataGridModel datagridModel = new DataGridModel()
            {
                total = rowCount,
                rows = pList,
                footer = null
            };

            return Json(datagridModel);
        }


        /// <summary>
        /// 加载 权限修改 窗体html  HttpGet
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditPermission(int ID)
        {
            // 根据id查询要修改的权限
            MODEL.ViewModel.Permission viewPermission = OperateContext.Current.BLLSession.IOu_PermissionBLL.GetListBy(p => p.pid == ID).FirstOrDefault().ToViewModel();
            //准备请求方式下拉框数据
            SetDropdownList();
            return PartialView(viewPermission);
        }


        #region 1.2 权限修改 +EditPermission(MODEL.ViewModel.Permission model)
        /// <summary>
        /// 1.2 权限修改POST
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditPermission(MODEL.Ou_Permission model)
        {
            string path=   Request.Path;
            int res = OperateContext.Current.BLLSession.IOu_PermissionBLL.Modify(model, "pName", "pAreaName", "pControllerName", "pActionName", "pFormMethod", "pOperationType", "pOrder", "pIsShow", "pRemark");

            if (res > 0)
                return Redirect("/admin/sys/Permission?ok");
            else
                return Redirect("/admin/sys/Permission?error");
        }
        #endregion

        /// <summary>
        /// 新增权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddPermission()
        {
            SetDropdownList();
            return PartialView("EditPermission"); ;
        }
        /// <summary>
        /// 新增权限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddPermission(MODEL.Ou_Permission model)
        {

            model.pAddTime = DateTime.Now;
            model.pIsDel = false;
            int res = OperateContext.Current.BLLSession.IOu_PermissionBLL.Add(model);
            if (res > 0)
                return Redirect("/admin/sys/Permission?ok");
            else
                return Redirect("/admin/sys/Permission?error");

        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        [Common.Attributes.AjaxRequest]
        public ActionResult DelPemission(int ID)
        {
            try
            {
                int res = OperateContext.Current.BLLSession.IOu_PermissionBLL.DelBy(p => p.pid == ID);
                if (res > 0)
                    return OperateContext.Current.RedirectAjax("ok", "删除成功~~~", null, "");
                else
                    return Redirect("/admin/sys/Permission?error");
            }
            catch (Exception ex)
            {
                return OperateContext.Current.RedirectAjax("err", "您现在删除的权限正在被角色使用，请先取消角色中的这个权限！", null, "");
            }


        }

        /// <summary>
        /// 查询子权限列表数据
        /// </summary>
        /// <param name="ID">子权限的父ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PermissionSon()
        {
            //获取父权限id
            int parentMenuId = int.Parse(Request.QueryString["pid"]);
            return View();
            
        }

        #region  查询子权限列表数据 + PermissionSon -post
        [HttpPost]
        /// <summary>
        /// 查询子权限列表数据
        /// </summary>
        /// <returns></returns>
        public ActionResult PermissionSon(FormCollection form)
        {
            //获取页容量
            int pageSize = int.Parse(Request.Form["rows"]);
            //获取请求的页码
            int pageIndex = int.Parse(Request.Form["page"]);
            //获取父权限id
            int parentMenuId = int.Parse(Request.QueryString["pid"]);

            // 查询分页数据
            var list = OperateContext.Current.BLLSession.IOu_PermissionBLL.GetPagedList(pageIndex, pageSize, p => p.pParent == parentMenuId && p.pIsDel == false, p => p.pOrder).Select(p => p.ToPOCO());
            int rowCount = OperateContext.Current.BLLSession.IOu_PermissionBLL.GetListBy(p => p.pParent == parentMenuId && p.pIsDel == false).Count();
            //2 生成规定格式的 json字符串发回 给 异步对象
            MODEL.EasyUIModel.DataGridModel dgModel = new MODEL.EasyUIModel.DataGridModel()
            {
                total = rowCount,
                rows = list,
                footer = null
            };
            return Json(dgModel);
        }
        #endregion

        /// <summary>
        /// 设置编辑页面的下拉框
        /// </summary>
        void SetDropdownList()
        {
            //准备请求方式下拉框数据
            ViewBag.httpMethodList = new List<SelectListItem>(){
              new SelectListItem(){ Text="Get", Value="1" },
              new SelectListItem(){Text="Post",Value="2"},
               new SelectListItem(){Text="Get & Post",Value="3"}
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

        }
    }
}
