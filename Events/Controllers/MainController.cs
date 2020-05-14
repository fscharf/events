using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Events.Models;
using System.Web.UI.HtmlControls;

namespace Events.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public void Authorize(Events.Models.cadastro cadastroModel)
        {
            using (eventsEntities db = new eventsEntities())
            {
                var cadastroDetails = db.cadastro.Where(x => x.email == cadastroModel.email && x.senha == cadastroModel.senha).FirstOrDefault();
                if (cadastroDetails == null)
                {
                    TempData["Error"] = "Usuário ou senha inválidos.";
                    Response.Redirect("/entrar");
                }
                else
                {
                    Session["id"] = cadastroDetails.id_cadastro;
                    Session["nome"] = cadastroDetails.nome;
                    Session["email"] = cadastroDetails.email;
                    Session["senha"] = cadastroDetails.senha;

                    Response.Redirect("/");
                }
            }
        }

        public void Logout()
        {
            Session.Abandon();
            Response.Redirect("/");
        }
    }
}