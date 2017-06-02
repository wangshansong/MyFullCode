using System.Web.Mvc;

namespace MVCOA.Logic.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Admin", id = UrlParameter.Optional },
                new string[1] { "MVCOA.Logic.Admin" }
            );

         
        }
    }
}
