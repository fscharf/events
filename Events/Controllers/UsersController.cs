using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Events.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        //public ActionResult Index()
        //{
        //    IEnumerable<USUARIO> userList;
        //    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Users").Result;
        //    userList = response.Content.ReadAsAsync<IEnumerable<USUARIO>>().Result;
        //    return View(userList);
        //}

        // GET:
        public ActionResult Register(int id = 0)
        {
            ViewBag.Title = "Crie sua conta grátis";
            var userModel = new USUARIO();
            return View(userModel);
        }

        // POST: /api/Users
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(USUARIO userModel)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Users", userModel).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Cadastro realizado com sucesso!";
                return Redirect("/");
            }
            else
            {
                TempData["Error"] = "E-mail já cadastrado.";
                return View(userModel);
            }
        }

        // GET:
        public ActionResult Login()
        {
            ViewBag.Title = "Iniciar sessão";
            return View();
        }

        // POST:
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(USUARIO userModel)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Users").Result;

            if (response.IsSuccessStatusCode && userModel.EMAIL != null && userModel.SENHA == GlobalVariables.CalculateMD5Hash(userModel.SENHA))
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, userModel.EMAIL),
                    new Claim(ClaimTypes.GivenName, userModel.NOME),
                    new Claim(ClaimTypes.Sid, userModel.COD_USUARIO + "")
                }, "ApplicationCookie");

                var context = Request.GetOwinContext();
                var authManager = context.Authentication;
                authManager.SignIn(identity);
                return Redirect("/");
            }
            else
            {
                TempData["Error"] = "Email ou senha inválidos.";
                return View(userModel);
            }

        }

        public ActionResult Logout()
        {
            var context = Request.GetOwinContext();
            var authManager = context.Authentication;
            authManager.SignOut("ApplicationCookie");
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return Redirect("/");
        }
    }
}