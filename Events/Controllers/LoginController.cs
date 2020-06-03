using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//Gabriel Teste commit okas
namespace Events.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        // Busca informações do Model para autenticar usuário e inicia sessão
        [HttpPost]
        public ActionResult Auth(User userModel)
        {
            using (UsersEntities db = new UsersEntities())
            {
                var userInfo = db.Users.Where(x => x.Email == userModel.Email && x.Password == userModel.Password).FirstOrDefault();
                if (userInfo == null)
                {
                    TempData["Error"] = "Email ou senha inválidos.";
                    return View("Login");
                }
                else
                {
                    Session["UserID"] = userModel.ID;
                    Session["Name"] = userModel.Name;
                    Session["Email"] = userModel.Email;
                    Session["Password"] = userModel.Password;
                    Session["IsAdmin"] = userModel.IsAdmin;
                    return RedirectToAction("Index", "Main");
                }
            }
        }

        // Encerra sessão do usuário autenticado
        public void Logout()
        {
            Session.Abandon();
            Response.Redirect("/");
        }
    }
}