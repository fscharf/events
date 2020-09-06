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
                TempData["Error"] = "Email já cadastrado.";
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
            var tokenBased = string.Empty;
            HttpResponseMessage response = GlobalVariables
                .WebApiClient.GetAsync("Login/ValidLogin?email=" + userModel.EMAIL + "&password=" + userModel.SENHA).Result;

            if (response.IsSuccessStatusCode)
            {
                var resultMessage = response.Content.ReadAsStringAsync().Result;
                tokenBased = JsonConvert.DeserializeObject<string>(resultMessage);
                Session["TokenNumber"] = tokenBased;
                Session["Email"] = userModel.EMAIL;

                return Content(tokenBased);
                //var identity = new ClaimsIdentity(new[]
                //                {
                //    new Claim(ClaimTypes.Email, userModel.EMAIL),
                //    new Claim(ClaimTypes.GivenName, userModel.NOME),
                //    new Claim(ClaimTypes.Sid, userModel.COD_USUARIO + "")
                //}, "ApplicationCookie");

                //Request.GetOwinContext().Authentication.SignIn(identity);
                //return Redirect("/");
            }
            else
            {
                TempData["Error"] = "Email ou senha inválidos.";
                return View(userModel);
            }
        }

        public async Task<ActionResult> GetUSUARIO()
        {
            string ReturnMessage = String.Empty;

            var client = GlobalVariables.WebApiClient;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                Session["TokenNumber"].ToString() + ":" + Session["Email"]);

            var response = await client.GetAsync("Login/GetUSUARIO");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                ReturnMessage = JsonConvert.DeserializeObject<string>(result);
            }
            return Content(ReturnMessage);
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
