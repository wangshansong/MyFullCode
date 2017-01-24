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
    public class OperateContext
    {
        /// <summary>
        /// 业务仓储
        /// </summary>
        public static IBLLSession BLLSession = DI.SpringHelper.GetObject<IBLLSession>("BLLSession");

        /// <summary>
        /// 根据用户的ID查询用户的权限
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<MODEL.Ou_Permission> GetUserPermission(int userID)
        {
            List<MODEL.Ou_Permission> userAllPermssion = new List<MODEL.Ou_Permission>();
            //1.0根据用户的ID查询用户的角色
            List<int> roleIdList = BLLSession.IOu_UserRoleBLL.GetListBy(u => u.urUId == userID).Select(u => u.urRId).ToList();
            //2.0根据角色ID查询用户的权限ID
            List<int> permissionIdList = BLLSession.IOu_RolePermissionBLL.GetListBy(p => roleIdList.Contains(p.rpRId)).Select(p => p.rpPId).ToList();
            //3.0根据权限ID查询具体的权限
            List<MODEL.Ou_Permission> permissionList = BLLSession.IOu_PermissionBLL.GetListBy(p => permissionIdList.Contains(p.pid)).Select(p => p.ToPOCO()).ToList();
            //4.0根据用户ID查询ViP权限ID
            List<int> vipPermissonIdList = BLLSession.IOu_UserVipPermissionBLL.GetListBy(vip => vip.vipUserId == userID).Select(vip => vip.vipPermission).ToList();
            //5.0根据VIP权限ID查询对应的VIP权限
            List<MODEL.Ou_Permission> vipPermissonList = BLLSession.IOu_PermissionBLL.GetListBy(p => vipPermissonIdList.Contains(p.pid)).Select(p => p.ToPOCO()).ToList();
            //6.0获得用户所有的权限= 普通权限+特权
            userAllPermssion.AddRange(permissionList);
            userAllPermssion.AddRange(vipPermissonList);
            return userAllPermssion;
        }
    }
}
