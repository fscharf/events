using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Events.Models;

namespace Events.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (UsersEntities userModel = new UsersEntities())
            {
                return View(userModel.Users.ToList());
            }
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                using (UsersEntities userModel = new UsersEntities())
                {
                    userModel.Users.Add(user);
                    userModel.SaveChanges();
                }
                TempData["Success"] = "Cadastro criado com sucesso!";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Ocorreu um erro inesperado. Tente novamente.";
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            using (UsersEntities userModel = new UsersEntities())
            {
                return View(userModel.Users.Where(x => x.ID == id).FirstOrDefault());
            }
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            try
            {
                using (UsersEntities userModel = new UsersEntities())
                {
                    userModel.Entry(user).State = EntityState.Modified;
                    userModel.SaveChanges();
                }
                TempData["Success"] = "Cadastro alterado com sucesso!";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Ocorreu um erro inesperado. Tente novamente.";
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            using (UsersEntities userModel = new UsersEntities())
            {
                return View(userModel.Users.Where(x => x.ID == id).FirstOrDefault());
            }
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormatException exception)
        {
            try
            {
                using (UsersEntities userModel = new UsersEntities())
                {
                    User user = userModel.Users.Where(x => x.ID == id).FirstOrDefault();
                    userModel.Users.Remove(user);
                    userModel.SaveChanges();
                }
                TempData["Success"] = "Cadastro excluído com sucesso!";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Ocorreu um erro inesperado. Tente novamente.";
                return View();
            }
        }
    }
}
