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
        public ActionResult Login(FormCollection collection)
        {
            AjaxMsgModel ajaxMsg = new AjaxMsgModel();
            //1.获取用户名密码
            string strName = collection["txtName"].ToString();
            string strPwd = collection["txtPwd"].ToString();
            MODEL.Ou_UserInfo user = OperateContext.BLLSession.IOu_UserInfoBLL.Login(strName, strPwd);
            if (user != null)
            {
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
