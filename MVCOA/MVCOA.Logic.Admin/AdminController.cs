using Common;
using MODEL.FormatModel;
using MVCOA.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCOA.Logic.Admin
{
    /// <summary>
    /// 管理员登录相关的类
    /// </summary>
    public class AdminController : Controller
    {
        /// <summary>
        /// 管理员登录 --HttpGet
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 管理员登录 -- HttpPost
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(MODEL.ViewModel.LoginUser userInfo)
        {
            if (!ModelState.IsValid) 
            {
                return OperateContext.Current.RedirectAjax("err", "没有权限！", null, "");
            }
            if (OperateContext.Current.LoginAdmin(userInfo))
            {
                return OperateContext.Current.RedirectAjax("ok", "登录成功！", null, "/admin/admin/Index");
            }
            else
            {
                return OperateContext.Current.RedirectAjax("err", "登录失败！", null, "");
            }

        }

        /// <summary>
        /// 显示主页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

    }
}
