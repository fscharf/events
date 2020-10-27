using Events.Models;
using Microsoft.Ajax.Utilities;
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
    public class UsersController : Controller
    {
        [AllowAnonymous]
        public ActionResult Register() => View();

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(USUARIO uSUARIO)
        {
            IEnumerable<USUARIO> userList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("users").Result;

            if (response.IsSuccessStatusCode)
            {
                userList = response.Content.ReadAsAsync<IEnumerable<USUARIO>>().Result;
                var emailExists = userList.Any(x => x.EMAIL == uSUARIO.EMAIL);

                if (emailExists)
                {
                    TempData["Error"] = "Email já cadastrado.";
                    return View(uSUARIO);
                }
                else
                {
                    response = GlobalVariables.WebApiClient.PostAsJsonAsync("users", uSUARIO).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        response = GlobalVariables.WebApiClient.GetAsync("users").Result;
                        userList = response.Content.ReadAsAsync<IEnumerable<USUARIO>>().Result;
                        var userDetails = userList.Where(x => x.EMAIL == uSUARIO.EMAIL).FirstOrDefault();

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

                        TempData["Success"] = "Cadastro realizado com sucesso!";
                        return Redirect("/");
                    }
                    else
                    {
                        TempData["Error"] = "Ocorreu um erro ao solicitar sua requisição.";
                        return View(uSUARIO);
                    }
                }
            }
            else
            {
                TempData["Error"] = "Ocorreu um erro ao solicitar sua requisição.";
                return View(uSUARIO);
            }
        }

        [AllowAnonymous]
        public ActionResult Login() => View();

        [AllowAnonymous]
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
                else if (userDetails.ATIVO == 0)
                {
                    TempData["Error"] = "Usuário não está ativo.";
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

                    if (identity.Claims.Any(c => c.Type == ClaimTypes.Role && (c.Value == "4")))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (identity.Claims.Any(c => c.Type == ClaimTypes.Role && (c.Value == "3")))
                    {
                        return RedirectToAction("Index", "Master");
                    }
                    else
                    {
                        return Redirect("/");
                    }
                }
            }
            else
            {
                TempData["Error"] = "Operação ilegal.";
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

        [Authorize]
        public ActionResult MyProfile(int id = 0)
        {
            var userAuth = (ClaimsIdentity)User.Identity;
            var identity = Convert.ToInt32(userAuth.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault().Value);

            if (id == 0)
            {
                return View(new USUARIO());
            }
            else if (id != identity)
            {
                TempData["Error"] = "Você não tem permissão para acessar essa página.";
                return Redirect("/Conta/" + identity.ToString());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("users/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<USUARIO>().Result);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyProfile(USUARIO uSUARIO, int id = 0)
        {
            if (id == 0)
            {
                TempData["Error"] = "Usuário não localizado.";
                return View(uSUARIO);
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("users/" + id.ToString(), uSUARIO).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Dados atualizados com sucesso!";
                    return RedirectToAction("MyProfile", "Users");
                }
                else
                {
                    TempData["Error"] = "Operação ilegal.";
                    return View(uSUARIO);
                }
            }
        }

        [Authorize]
        public ActionResult DesactivateAccount(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("users/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var context = Request.GetOwinContext();
                var authManager = context.Authentication;
                authManager.SignOut("ApplicationCookie");
                Session.Abandon();
                Session.Clear();
                Session.RemoveAll();

                TempData["Success"] = "Sua conta foi desativada com êxito.";
                return RedirectToAction("Index", "Main");
            }
            else
            {
                TempData["Error"] = "Ocorreu um erro ao efetuar sua requisição.";
                return View("MyProfile");
            }
        }
    }
}
