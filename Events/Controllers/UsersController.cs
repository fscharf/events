﻿using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Users").Result;
            userList = response.Content.ReadAsAsync<IEnumerable<USUARIO>>().Result;
            return View(userList);
        }

        public ActionResult Register(int id = 0)
        {
            ViewBag.Title = "Crie sua conta grátis";
            var usrModel = new USUARIO();
            return View(usrModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(USUARIO usrModel)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Users", usrModel).Result;

            return RedirectToAction("Index");
        }
    }
}