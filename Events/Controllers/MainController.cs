using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Events.Models;
using System.Web.UI.HtmlControls;

namespace Events.Controllers
{
    //Teste de commit: Miguel
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}