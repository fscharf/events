using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Events.Controllers
{
    public class CadastrosController : Controller
    {

        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public void Criar()
        {
            var cadastro = new Cadastro();
            cadastro.nome = Request["nome"];
            cadastro.cpf = Request["cpf"];
            cadastro.email = Request["email"];
            cadastro.senha = Request["senha"];
            cadastro.Salvar();

            Session["id"] = cadastro.id;
            Session["nome"] = cadastro.nome;
            Session["cpf"] = cadastro.cpf;
            Session["email"] = cadastro.email;
            Session["senha"] = cadastro.senha;

            Response.Redirect("/");
        }
    }
}