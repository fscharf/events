using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Events.Models;
using System.Web.UI.HtmlControls;
using System.Net.Http;
using PagedList;

namespace Events.Controllers
{
    [AllowAnonymous]
    public class MainController : Controller
    {
        public ActionResult Index(int? page)
        {
            IEnumerable<EVENTO> eventList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("events").Result;
            eventList = response.Content.ReadAsAsync<IEnumerable<EVENTO>>().Result;
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            eventList = eventList.Where(x => x.ATIVO == 1);

            return View(eventList.ToPagedList(pageNumber, pageSize));
        }
    }
}