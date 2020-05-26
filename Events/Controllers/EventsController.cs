using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Events.Controllers
{
    public class EventsController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Event()
        {
            return View();
        }

        public ActionResult Confirm()
        {
            return View();
        }

    }
}