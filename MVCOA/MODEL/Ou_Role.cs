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
    
    public partial class Ou_Role
    {
        public Ou_Role()
        {
            this.Ou_RolePermission = new HashSet<Ou_RolePermission>();
            this.Ou_UserRole = new HashSet<Ou_UserRole>();
        }
    
        public int rId { get; set; }
        public int rDepId { get; set; }
        public string rName { get; set; }
        public string rRemark { get; set; }
        public bool rIsShow { get; set; }
        public bool rIsDel { get; set; }
        public System.DateTime rAddTime { get; set; }
    
        public virtual Ou_Department Ou_Department { get; set; }
        public virtual ICollection<Ou_RolePermission> Ou_RolePermission { get; set; }
        public virtual ICollection<Ou_UserRole> Ou_UserRole { get; set; }
    }
}
