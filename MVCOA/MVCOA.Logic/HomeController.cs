using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVCOA.Helper;

namespace MVCOA.Logic
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/admin/admin/login");
        }
    }
}
