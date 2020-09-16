using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Events.Models;
using System.Web.UI.HtmlControls;
using System.Net.Http;

namespace Events.Controllers
{
    [AllowAnonymous]
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<EVENTO> eventList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("events").Result;
            eventList = response.Content.ReadAsAsync<IEnumerable<EVENTO>>().Result;
            return View(eventList);
        }
    }
}