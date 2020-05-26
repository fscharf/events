using Events.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Events.Controllers
{
    public class RegisterController : Controller
    {
        [HttpGet]
        public ActionResult Register()
        {
            User userModel = new User();
            return View(userModel);
        }

        [HttpPost]
        public void Create(User userModel)
        {
            using (LoginDbEntities dbModel = new LoginDbEntities())
            {
                try
                {
                    if (dbModel.Users.Any(x => x.LoginEmail != userModel.LoginEmail))
                    {
                        dbModel.Users.Add(userModel);
                        dbModel.SaveChanges();

                        Session["userID"] = userModel.UserID;
                        Session["userName"] = userModel.LoginName;
                        Session["userEmail"] = userModel.LoginEmail;
                        Session["userPass"] = userModel.PasswordHash;

                        ModelState.Clear();
                        TempData["Success"] = "Cadastro realizado com sucesso.";
                        new User();
                        Response.Redirect("/");
                    }
                }
                catch (Exception)
                {
                    TempData["ErrorEmail"] = "E-mail já cadastrado.";
                    Response.Redirect("/cadastro");
                }
            }
        }
    }
}