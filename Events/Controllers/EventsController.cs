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
    public class EventsController : Controller
    {
        // exemplo git
        // teste okay
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

        public ActionResult ListEvent()
        {
            IEnumerable<EVENTO> EventoList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Evento").Result;
            EventoList = response.Content.ReadAsAsync<IEnumerable<EVENTO>>().Result;
            return View(EventoList);
        }

  }
}