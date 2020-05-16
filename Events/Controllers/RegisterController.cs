using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Events.Controllers
{
    public class RegisterController : Controller
    {
        public ActionResult PreRegister()
        {
            return View();
        }

        public ActionResult Student()
        {
            return View();
        }

        public ActionResult Guest()
        {
            return View();
        }
        
        // Deletar após criar as 3 views acima
        public ActionResult Register()
        {
            return View();
        }

        // Ao efetuar submit, cria um novo cadastro e inicia sessão
        [HttpPost]
        public void Create()
        {
            var cadastro = new Register();
            cadastro.nome = Request["nome"];
            cadastro.cpf = Request["cpf"];
            cadastro.email = Request["email"];
            cadastro.senha = Request["senha"];
            cadastro.Save();

            Session["id"] = cadastro.id;
            Session["nome"] = cadastro.nome;
            Session["cpf"] = cadastro.cpf;
            Session["email"] = cadastro.email;
            Session["senha"] = cadastro.senha;

            Response.Redirect("/");
        }
    }
}