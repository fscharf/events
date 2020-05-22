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
        public ActionResult Register(int id = 0)
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

                        Session["Id"] = userModel.UserID;
                        Session["Name"] = userModel.LoginName;
                        Session["Email"] = userModel.LoginEmail;

                        ModelState.Clear();
                        TempData["Success"] = "Cadastro realizado com sucesso.";
                        new User();
                        Response.Redirect("/");
                    }
                }
                catch (Exception e)
                {
                    TempData["ErrorEmail"] = "E-mail já cadastrado.";
                    Response.Redirect("/cadastro");
                }
            }
        }
    }
}