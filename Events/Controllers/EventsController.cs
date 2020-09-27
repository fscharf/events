using Events.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Events.Controllers
{
    [AllowAnonymous]
    public class EventsController : Controller
    {
        public ActionResult Index(int? page, string currentFilter, string searchString, string searchDate)
        {
            IEnumerable<EVENTO> eventList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("events").Result;
            eventList = response.Content.ReadAsAsync<IEnumerable<EVENTO>>().Result;
            if (searchString != null || searchDate != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                eventList = eventList.Where(x => x.TITULO.Contains(searchString)
                                            || x.DESCRICAO.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(searchDate))
            {
                eventList = eventList.Where(x => x.DATA.Equals(Convert.ToDateTime(searchDate)));         
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(eventList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return View(new EVENTO());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("events/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<EVENTO>().Result);
            }
        }

        [Authorize(Roles = "1,2")]
        public ActionResult Confirm()
        {
            ViewBag.Title = "Inscrição realizada com sucesso";
            return View();
        }

        [Authorize(Roles = "1,2")]
        public ActionResult MyEvents()
        {
            ViewBag.Title = "Meus eventos";
            return View();
        }
    }
}