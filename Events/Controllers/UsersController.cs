using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Events.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            IEnumerable<USUARIO> userList;
            HttpResponseMessage res = GlobalVariables.WebApiClient.GetAsync("Users").Result;
            userList = res.Content.ReadAsAsync<IEnumerable<USUARIO>>().Result;
            return View(userList);
        }
    }
}