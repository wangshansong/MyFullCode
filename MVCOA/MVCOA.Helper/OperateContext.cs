using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBLL;
using DI;
using System.Web;
using System.Web.SessionState;
using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;
using MODEL.EasyUIModel;
using MODEL.FormatModel;

namespace MVCOA.Helper
{
    /// <summary>
    /// 通过容器生成业务层仓储
    /// </summary>
    public class OperateContext
    {

        const string Admin_CookiePath = "/admin/";
        /// <summary>
        /// 存储Ou_UserInfo的Session的键
        /// </summary>
        const string Admin_InfoKey = "aInfo";
        /// <summary>
        /// 存储用户权限信息Session的键
        /// </summary>
        const string Admin_PermissionKey = "aPermission";
        const string Admin_TreeString = "aTreeString";
        const string Admin_LogicSessionKey = "BLLSession";

        #region 0.1 Http上下文 及 相关属性
        /// <summary>
        /// Http上下文
        /// </summary>
        HttpContext ContextHttp
        {
            get
            {
                return HttpContext.Current;
            }
        }

        HttpResponse Response
        {
            get
            {
                return ContextHttp.Response;
            }
        }

        HttpRequest Request
        {
            get
            {
                return ContextHttp.Request;
            }
        }

        HttpSessionState Session
        {
            get
            {
                return ContextHttp.Session;
            }
        }
        #endregion

        #region 0.2 用户权限 +  List<MODEL.Ou_Permission> UserPermission
        /// <summary>
        /// 用户权限
        /// </summary>
        public List<MODEL.Ou_Permission> UserPermission
        {
            get
            {
                return Session[Admin_PermissionKey] as List<MODEL.Ou_Permission>;
            }
            set
            {
                Session[Admin_PermissionKey] = value;
            }
        }
        #endregion



        #region 0.3 业务仓储 + IBLLSession BLLSession;
        /// <summary>
        /// 业务仓储
        /// </summary>
        public IBLLSession BLLSession;
        #endregion

        #region 0.4 当前用户对象 +MODEL.Ou_UserInfo CurrentUser
        /// <summary>
        /// 当前用户对象
        /// </summary>
        public MODEL.Ou_UserInfo CurrentUser
        {
            get
            {
                return Session[Admin_InfoKey] as MODEL.Ou_UserInfo;
            }
            set
            {
                Session[Admin_InfoKey] = value;
            }
        }
        #endregion

        #region 0.5 实例构造函数 初始化 业务仓储
        public OperateContext()
        {
            BLLSession = DI.SpringHelper.GetObject<IBLLSession>(Admin_LogicSessionKey);
        }
        #endregion

        #region 0.6 获取当前 操作上下文 + OperateContext Current
        /// <summary>
        /// 获取当前 操作上下文 (为每个处理浏览器请求的服务器线程 单独创建 操作上下文)
        /// </summary>
        public static OperateContext Current
        {
            get
            {
                string strOperateContextName = typeof(OperateContext).FullName;
                OperateContext oContext = CallContext.GetData(strOperateContextName) as OperateContext;
                if (oContext == null)
                {
                    oContext = new OperateContext();
                    CallContext.SetData(strOperateContextName, oContext);

                }
                return oContext;
            }
        }
        #endregion

        #region 0.7 获取当前登陆用户的权限树Json字符串 +string UserTreeJsonStr
        /// <summary>
        /// 获取当前登陆用户的权限树Json字符串
        /// </summary>
        public string UserTreeJsonStr
        {
            get
            {
                if (Session[Admin_TreeString] == null)
                {
                    List<TreeNode> nodeList = MODEL.Ou_Permission.ToTreeNodes(UserPermission.FindAll(p => p.pIsShow == true));
                    Session[Admin_TreeString] = Common.DataHelper.ObjToJson(nodeList);
                }
                return Session[Admin_TreeString].ToString();
            }
        }
        #endregion


        //---------------------------------------------2.0 登陆权限 等系统操作--------------------
        #region 2.0 根据用户的 ID查询用户的权限  +  List<MODEL.Ou_Permission> GetUserPermission(int userID)
        /// <summary>
        /// 根据用户的 ID查询用户的权限
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<MODEL.Ou_Permission> GetUserPermission(int userID)
        {
            List<MODEL.Ou_Permission> userAllPermssion = new List<MODEL.Ou_Permission>();
            //1.0根据用户的ID查询用户的角色
            List<int> roleIdList = Current.BLLSession.IOu_UserRoleBLL.GetListBy(u => u.urUId == userID).Select(u => u.urRId).ToList();
            //2.0根据角色ID查询用户的权限ID
            List<int> permissionIdList = Current.BLLSession.IOu_RolePermissionBLL.GetListBy(p => roleIdList.Contains(p.rpRId)).Select(p => p.rpPId).ToList();
            //3.0根据权限ID查询具体的权限
            List<MODEL.Ou_Permission> permissionList = Current.BLLSession.IOu_PermissionBLL.GetListBy(p => permissionIdList.Contains(p.pid)).Select(p => p.ToPOCO()).ToList();
            //4.0根据用户ID查询ViP权限ID
            List<int> vipPermissonIdList = Current.BLLSession.IOu_UserVipPermissionBLL.GetListBy(vip => vip.vipUserId == userID).Select(vip => vip.vipPermission).ToList();
            //5.0根据VIP权限ID查询对应的VIP权限
            List<MODEL.Ou_Permission> vipPermissonList = Current.BLLSession.IOu_PermissionBLL.GetListBy(p => vipPermissonIdList.Contains(p.pid)).Select(p => p.ToPOCO()).ToList();
            //6.0获得用户所有的权限= 普通权限+特权
            userAllPermssion.AddRange(permissionList);
            userAllPermssion.AddRange(vipPermissonList);
            return userAllPermssion;
        }
        #endregion

        #region 2.1 管理员登录方法 + bool LoginAdmin(MODEL.ViewModel.LoginUser usrPara)
        /// <summary>
        /// 管理员登录方法
        /// </summary>
        /// <param name="usrPara"></param>
        public bool LoginAdmin(MODEL.ViewModel.LoginUser usrPara)
        {
            //到业务成查询
            MODEL.Ou_UserInfo usr = BLLSession.IOu_UserInfoBLL.Login(usrPara.uLoginName, usrPara.uPwd);
            if (usr != null)
            {
                //2.1 保存 用户数据(Session or Cookie)
                CurrentUser = usr;
                //如果选择了复选框，则要使用cookie保存数据
                if (usrPara.isAlways)
                {
                    //2.1.2将用户id加密成字符串
                    string strCookieValue = Common.SecurityHelper.EncryptUserInfo(usr.uId.ToString());
                    //2.1.3创建cookie
                    HttpCookie cookie = new HttpCookie(Admin_InfoKey, strCookieValue);
                    cookie.Expires = DateTime.Now.AddDays(1);
                    cookie.Path = Admin_CookiePath;
                    Response.Cookies.Add(cookie);
                }
                //2.2 查询当前用户的 权限，并将权限 存入 Session 中
                UserPermission = GetUserPermission(usr.uId);
                return true;
            }
            return false;
        }
        #endregion

        #region 2.2 判断当前用户是否登陆 +bool IsLogin()
        public bool IsLogin()
        {
            if (Session[Admin_InfoKey] == null)
            {
                if (Request.Cookies[Admin_InfoKey] == null)
                {
                    return false;
                }
                else//如果有cookie则从cookie中获取用户id并查询相关数据存入 Session
                {
                    string strUserInfo = Request.Cookies[Admin_InfoKey].Value;
                    strUserInfo = Common.SecurityHelper.DecryptUserInfo(strUserInfo);
                    int userId = int.Parse(strUserInfo);
                    MODEL.Ou_UserInfo usr = BLLSession.IOu_UserInfoBLL.GetListBy(u => u.uId == userId).First();
                    CurrentUser = usr;
                    UserPermission = OperateContext.Current.GetUserPermission(usr.uId);
                }
            }
            return true;
        }
        #endregion



        //---------------------------------------------3.0 公用操作方法--------------------
        #region 3.1 生成 Json 格式的返回值 +ActionResult RedirectAjax(string statu, string msg, object data, string backurl)
        /// <summary>
        /// 生成 Json 格式的返回值
        /// </summary>
        /// <param name="statu">状态 ok：成功 、 err：失败</param>
        /// <param name="msg">消息内容</param>
        /// <param name="data">返回的数据</param>
        /// <param name="backurl">跳转地址URL</param>
        /// <returns></returns>
        public ActionResult RedirectAjax(string statu, string msg, object data, string backurl)
        {
            AjaxMsgModel ajaxMsg = new AjaxMsgModel()
              {
                  Statu = statu,
                  Msg = msg,
                  Data = data,
                  BackUrl = backurl
              };
            string strJsonAjaxMsg = Common.JsonHelper<AjaxMsgModel>.ModelToJsonString(ajaxMsg);
            ContentResult contentResult = new ContentResult();
            contentResult.Content = strJsonAjaxMsg;
            return contentResult;
        }
        #endregion

        #region 3.2 重定向 + ActionResult Redirect(string url, ActionDescriptor action)
        /// <summary>
        /// 重定向
        /// </summary>
        /// <param name="url">跳转地址</param>
        /// <param name="action">提供有关操作方法的信息，如操作方法的名称、控制器、参数、特性和筛选器。</param>
        /// <returns></returns>
        public ActionResult Redirect(string url, ActionDescriptor action)
        {
            if (action.IsDefined(typeof(Common.Attributes.AjaxRequestAttribute), false)
                || action.ControllerDescriptor.IsDefined(typeof(Common.Attributes.AjaxRequestAttribute), false)
                )
            {
                return RedirectAjax("nologin", "您没有登陆或没有权限访问此页面~~", null, url);
            }
            else
            {
                return new RedirectResult(url);
            }
        }
        #endregion

        #region 3.3 判断是否有权限 +  bool HasPemission(string areaName, string controllerName, string actionName, string httpMethod)
        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <param name="areaName">区域名</param>
        /// <param name="controllerName">控制器名</param>
        /// <param name="actionName">方法名</param>
        /// <param name="httpMethod">访问方式Get/Post</param>
        /// <returns></returns>
        public bool HasPemission(string areaName, string controllerName, string actionName, string httpMethod)
        {
            var listP = from per in UserPermission
                        where string.Equals(per.pAreaName, areaName, StringComparison.CurrentCultureIgnoreCase)
                        && string.Equals(per.pControllerName, controllerName, StringComparison.CurrentCultureIgnoreCase)
                        && string.Equals(per.pActionName, actionName, StringComparison.CurrentCultureIgnoreCase)
                        && (per.pFormMethod == (httpMethod.ToLower() == "get" ? 1 : 2) || per.pFormMethod == 3)
                        select per;
            return listP.Count() > 0;
        }
        #endregion
    }
}
