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
            var register = new Register();
            register.nome = Request["nome"];
            register.cpf = Request["cpf"];
            register.email = Request["email"];
            register.senha = Request["senha"];
            register.Save();

            Session["id"] = register.id;
            Session["nome"] = register.nome;
            Session["cpf"] = register.cpf;
            Session["email"] = register.email;
            Session["senha"] = register.senha;

            Response.Redirect("/");
        }
    }
}