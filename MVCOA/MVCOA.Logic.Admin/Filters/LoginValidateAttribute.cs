using MVCOA.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVCOA.Logic.Admin.Filters
{
    /// <summary>
    /// 管理员 身份验证 过滤器( 授权过滤器 首先运行，在任何其它过滤器或动作方法之前)
    /// </summary>
    public class LoginValidateAttribute : AuthorizeAttribute
    {
        /// <summary>
        ///  验证方法 - 在 ActionExcuting过滤器之前执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //1.如果请求的 Admin 区域里的 控制器类和方法，那么就要验证权限
            if (filterContext.RouteData.DataTokens.Keys.Contains("area")  //当前请求匹配的 路由对象中 是否 有 area区域
                && filterContext.RouteData.DataTokens["area"].ToString().ToLower() == "admin") //监测区域名 是否为 admin
            {
                //2.检查 被请求的 方法 和 控制器是否有 Skip 标签，如果有，则不验证；如果没有，则验证
                if (!filterContext.ActionDescriptor.IsDefined(typeof(Common.Attributes.SkipAttribute), false)
                  && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(Common.Attributes.SkipAttribute), false))
                {
                    //1.验证用户是否登陆(Session && Cookie)
                    if (!OperateContext.Current.IsLogin())
                    {
                        filterContext.Result= OperateContext.Current.Redirect("/admin/admin/login",filterContext.ActionDescriptor);
                    }
                    else  //2.验证登陆用户 是否有访问该页面的权限
                    {
                        string strAreaeName = filterContext.RouteData.DataTokens["area"].ToString().ToLower();
                        string strControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
                        string strActionName = filterContext.ActionDescriptor.ActionName.ToLower();
                        string strHttMethod = filterContext.HttpContext.Request.HttpMethod;
                        if (!OperateContext.Current.HasPemission(strAreaeName,strControllerName,strActionName,strHttMethod))
                        {
                            filterContext.Result = OperateContext.Current.Redirect("/admin/admin/login?msg=noPermission",filterContext.ActionDescriptor)   ;
                        }

                    }

                }
            }
        }

    }
}
