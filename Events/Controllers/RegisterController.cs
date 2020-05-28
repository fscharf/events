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
        public ActionResult Create(User userModel)
        {
            using (UsersEntities db = new UsersEntities())
            {
                if (db.Users.Any(x => x.Email == userModel.Email))
                {
                    TempData["Error"] = "Email já cadastrado.";
                    return View("Register", userModel);
                }
                else
                {
                    db.Users.Add(userModel);
                    db.SaveChanges();

                    Session["userID"] = userModel.ID;
                    Session["userName"] = userModel.Name;
                    Session["userEmail"] = userModel.Email;
                    Session["userPass"] = userModel.Password;
                }
            }
            ModelState.Clear();
            TempData["Success"] = "Cadastro realizado com sucesso.";
            return RedirectToAction("Index", "Main", new User());
        }
    }
}
