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
            AjaxMsgModel ajaxMsg = new AjaxMsgModel { Statu = "err", Msg = "登录失败" };

            if (OperateContext.Current.LoginAdmin(userInfo))
            {
                //1.4返回登录成功信息
                ajaxMsg.Msg = "登录成功！";
                ajaxMsg.Statu = "ok";
                ajaxMsg.BackUrl = "/admin/admin/Index";

            }
            else
            {
                ajaxMsg.Msg = "登录失败！";
                ajaxMsg.Statu = "err";

            }

            return Json(ajaxMsg);
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
