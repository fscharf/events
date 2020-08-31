using Events.Models;
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
            usrModel.SENHA = GlobalVariables.GetHash(usrModel.SENHA);

            // Código não funcionando
            var client = GlobalVariables.WebApiClient;
            string inputJson = Newtonsoft.Json.JsonConvert.SerializeObject(usrModel);
            HttpContent content = new StringContent(inputJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("https://localhost:44390/api/Users", content).Result;
                
            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Salvo com sucesso.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Ocorreu um erro inesperado.";
                return View("Register");
            }
        }
    }
}