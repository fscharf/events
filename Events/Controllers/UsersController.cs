using Events.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Events.Controllers
{
    [AllowAnonymous]
    public class UsersController : Controller
    {
        public ActionResult Register(int id = 0)
        {
            ViewBag.Title = "Crie sua conta grátis";
            var userModel = new USUARIO();
            return View(userModel);
        }

        // POST: /api/Users
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(USUARIO uSUARIO)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("users", uSUARIO).Result;
            if (response.IsSuccessStatusCode)
            {
                var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Email, uSUARIO.EMAIL),
                        new Claim(ClaimTypes.GivenName, uSUARIO.NOME),
                        new Claim(ClaimTypes.HomePhone, uSUARIO.CELULAR),
                        new Claim(ClaimTypes.Role, uSUARIO.COD_PERFIL.ToString()),
                        new Claim(ClaimTypes.Sid, uSUARIO.COD_USUARIO.ToString())
                    }, "ApplicationCookie");

                var context = Request.GetOwinContext();
                var authManager = context.Authentication;
                authManager.SignIn(identity);

                TempData["Success"] = "Cadastro realizado com sucesso!";
                return Redirect("/");
            }
            else
            {
                TempData["Error"] = "Email já cadastrado.";
                return View(uSUARIO);
            }
        }

        // GET:
        public ActionResult Login()
        {
            ViewBag.Title = "Iniciar sessão";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(USUARIO uSUARIO)
        {
            IEnumerable<USUARIO> userList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("users").Result;

            if (response.IsSuccessStatusCode)
            {
                userList = response.Content.ReadAsAsync<IEnumerable<USUARIO>>().Result;
                var userDetails = userList.Where(x => x.EMAIL == uSUARIO.EMAIL && x.SENHA == GlobalVariables.CalculateMD5Hash(uSUARIO.SENHA)).FirstOrDefault();

                if (userDetails == null)
                {
                    TempData["Error"] = "Email ou senha inválidos.";
                    return View(uSUARIO);
                }
                else
                {
                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Email, userDetails.EMAIL),
                        new Claim(ClaimTypes.GivenName, userDetails.NOME),
                        new Claim(ClaimTypes.HomePhone, userDetails.CELULAR),
                        new Claim(ClaimTypes.Role, userDetails.COD_PERFIL.ToString()),
                        new Claim(ClaimTypes.Sid, userDetails.COD_USUARIO.ToString())
                    }, "ApplicationCookie");

                    var context = Request.GetOwinContext();
                    var authManager = context.Authentication;
                    authManager.SignIn(identity);

                    return Redirect("/");
                }
            }
            else 
            {
                TempData["Error"] = "Ocorreu um erro inesperado. Favor contatar o administrador.";
                return View();
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

        public ActionResult Profile()
        {
            return View();
        }

        [Authorize(Roles = "1, 2")]
        public ActionResult UpdatePassword(int id = 0)
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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePassword(USUARIO uSUARIO, int id = 0)
        {
            if (id == 0)
            {
                TempData["Error"] = "Ocorreu um erro inesperado.";
                return View(uSUARIO);
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("users/" + id.ToString(), uSUARIO).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Senha atualizada com sucesso!";
                    return RedirectToAction("Profile", "Users");
                }
                else
                {
                    TempData["Error"] = "Ocorreu um erro inesperado.";
                    return View(uSUARIO);
                }
            }
        }
    }
}
