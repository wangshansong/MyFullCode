//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MODEL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ou_Permission
    {
        public Ou_Permission()
        {
            this.Ou_RolePermission = new HashSet<Ou_RolePermission>();
            this.Ou_UserVipPermission = new HashSet<Ou_UserVipPermission>();
        }
    
        public int pid { get; set; }
        public int pParent { get; set; }
        public string pName { get; set; }
        public string pAreaName { get; set; }
        public string pControllerName { get; set; }
        public string pActionName { get; set; }
        public short pFormMethod { get; set; }
        public short pOperationType { get; set; }
        public string pIco { get; set; }
        public int pOrder { get; set; }
        public bool pIsLink { get; set; }
        public string pLinkUrl { get; set; }
        public bool pIsShow { get; set; }
        public string pRemark { get; set; }
        public bool pIsDel { get; set; }
        public System.DateTime pAddTime { get; set; }
    
        public virtual ICollection<Ou_RolePermission> Ou_RolePermission { get; set; }
        public virtual ICollection<Ou_UserVipPermission> Ou_UserVipPermission { get; set; }
    }
}
