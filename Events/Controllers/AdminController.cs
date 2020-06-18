using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Events.Models;
using Microsoft.Ajax.Utilities;

namespace Events.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<User> userList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Users").Result;
            userList = response.Content.ReadAsAsync<IEnumerable<User>>().Result;
            return View(userList);
        }

        public ActionResult Add(int id = 0)
        {
            if (id == 0)
            {
                return View(new User());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Users/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<User>().Result);
            }
        }

        [HttpPost]
        public ActionResult Add(User user)
        {
            if (user.ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Users", user).Result;
                TempData["Success"] = "Cadastro criado com sucesso!";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Users/" + user.ID, user).Result;
                TempData["Success"] = "Cadastro atualizado com sucesso.";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Users/" + id.ToString()).Result;
            TempData["Success"] = "Cadastro excluído com sucesso.";
            return RedirectToAction("Index");
        }

        public ActionResult NewEvent()
        {
            return View();
        }
    }
}
