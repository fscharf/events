using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
//samuel teste commit okay
//Gabriel Teste commit okas
namespace Events.Controllers
{
    [AllowAnonymous]
    public class EventsController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Todos eventos";

            IEnumerable<EVENTO> eventList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("events").Result;
            eventList = response.Content.ReadAsAsync<IEnumerable<EVENTO>>().Result;
            return View(eventList);
        }

        public ActionResult Event()
        {
            return View();
        }

        public ActionResult Confirm()
        {
            ViewBag.Title = "Inscrição realizada com sucesso";
            return View();
        }
        public ActionResult MyEvents()
        {
            ViewBag.Title = "Meus eventos";
            return View();
        }

        public ActionResult ListEvent()
        {
            IEnumerable<EVENTO> eventList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("events").Result;
            eventList = response.Content.ReadAsAsync<IEnumerable<EVENTO>>().Result;
            return View(eventList);
        }

    }
}