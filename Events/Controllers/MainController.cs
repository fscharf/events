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

        // Ao efetuar submit compara dados registrados no database e valida a sessão
        [HttpPost]
        public void Authorize(Events.Models.cadastro registerModel)
        {
            using (eventsEntities db = new eventsEntities())
            {
                var registerDetails = db.cadastro.Where(x => x.email == registerModel.email && x.senha == registerModel.senha).FirstOrDefault();
                if (registerDetails == null)
                {
                    TempData["Error"] = "Usuário ou senha inválidos.";
                    Response.Redirect("/entrar");
                }
                else
                {
                    Session["id"] = registerDetails.id;
                    Session["nome"] = registerDetails.nome;
                    Session["email"] = registerDetails.email;
                    Session["senha"] = registerDetails.senha;

                    Response.Redirect("/");
                }
            }
        }

        // Encerra a sessão
        public void Logout()
        {
            Session.Abandon();
            Response.Redirect("/");
        }
    }
}