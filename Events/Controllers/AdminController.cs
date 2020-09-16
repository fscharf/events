using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Events.Models;

namespace Events.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // Admin for Events
        public ActionResult EventsList()
        {
            IEnumerable<EVENTO> eventList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("events").Result;
            eventList = response.Content.ReadAsAsync<IEnumerable<EVENTO>>().Result;
            return View(eventList);
        }

        public ActionResult EventDetails(int id = 0)
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

        // Admin for Users
        public ActionResult UsersList()
        {
            IEnumerable<USUARIO> userList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("users").Result;
            userList = response.Content.ReadAsAsync<IEnumerable<USUARIO>>().Result;
            return View(userList);
        }

        public ActionResult UserDetails(int id = 0)
        {
            if (id == 0)
            {
                return View(new USUARIO());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("users/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<USUARIO>().Result);
            }
        }
    }
}