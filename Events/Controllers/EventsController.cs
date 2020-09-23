using Events.Models;
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
        public ActionResult Index()
        {
            IEnumerable<EVENTO> eventList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("events").Result;
            eventList = response.Content.ReadAsAsync<IEnumerable<EVENTO>>().Result;
            return View(eventList);
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

        [Authorize(Roles = "1, 2")]
        public ActionResult Confirm()
        {
            ViewBag.Title = "Inscrição realizada com sucesso";
            return View();
        }

        [Authorize(Roles = "1, 2")]
        public ActionResult MyEvents()
        {
            ViewBag.Title = "Meus eventos";
            return View();
        }
    }
} 