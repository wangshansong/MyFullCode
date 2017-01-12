
 
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using DALB;

namespace DAL
{
	public partial class Bill_LeaveDAL : BaseDAL<MODEL.Bill_Leave>,IBill_LeaveDAL
    {
    }

	public partial class Ou_DepartmentDAL : BaseDAL<MODEL.Ou_Department>,IOu_DepartmentDAL
    {
    }

	public partial class Ou_PermissionDAL : BaseDAL<MODEL.Ou_Permission>,IOu_PermissionDAL
    {
    }

	public partial class Ou_RoleDAL : BaseDAL<MODEL.Ou_Role>,IOu_RoleDAL
    {
    }

	public partial class Ou_RolePermissionDAL : BaseDAL<MODEL.Ou_RolePermission>,IOu_RolePermissionDAL
    {
    }

	public partial class Ou_UserInfoDAL : BaseDAL<MODEL.Ou_UserInfo>,IOu_UserInfoDAL
    {
    }

	public partial class Ou_UserRoleDAL : BaseDAL<MODEL.Ou_UserRole>,IOu_UserRoleDAL
    {
    }

	public partial class Ou_UserVipPermissionDAL : BaseDAL<MODEL.Ou_UserVipPermission>,IOu_UserVipPermissionDAL
    {
    }

	public partial class sysdiagramDAL : BaseDAL<MODEL.sysdiagram>,IsysdiagramDAL
    {
    }

	public partial class WF_AutoTransactNodeDAL : BaseDAL<MODEL.WF_AutoTransactNode>,IWF_AutoTransactNodeDAL
    {
    }

	public partial class WF_BillFlowNodeDAL : BaseDAL<MODEL.WF_BillFlowNode>,IWF_BillFlowNodeDAL
    {
    }

	public partial class WF_BillFlowNodeRemarkDAL : BaseDAL<MODEL.WF_BillFlowNodeRemark>,IWF_BillFlowNodeRemarkDAL
    {
    }

	public partial class WF_BillStateDAL : BaseDAL<MODEL.WF_BillState>,IWF_BillStateDAL
    {
    }

	public partial class WF_NodeDAL : BaseDAL<MODEL.WF_Node>,IWF_NodeDAL
    {
    }

	public partial class WF_NodeStateDAL : BaseDAL<MODEL.WF_NodeState>,IWF_NodeStateDAL
    {
    }

	public partial class WF_WorkFlowDAL : BaseDAL<MODEL.WF_WorkFlow>,IWF_WorkFlowDAL
    {
    }

	public partial class WF_WorkFlowNodeDAL : BaseDAL<MODEL.WF_WorkFlowNode>,IWF_WorkFlowNodeDAL
    {
    }


}