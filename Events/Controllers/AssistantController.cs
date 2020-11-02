using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Events.Models;

namespace Events.Controllers
{
    [Authorize(Roles = "5")]
    public class AssistantController : Controller
    {
        public ActionResult Index() => View();

        public ActionResult Validate(string search)
        {
            IEnumerable<INSCRICAO> subsList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("subs").Result;
            subsList = response.Content.ReadAsAsync<IEnumerable<INSCRICAO>>().Result;

            if (!String.IsNullOrEmpty(search))
            {
                subsList = subsList.Where(x => x.COD_INSCRICAO.ToString().Equals(search));
            }

            ViewBag.CurrentFilter = search;

            IEnumerable<EVENTO> eventsList;
            response = GlobalVariables.WebApiClient.GetAsync("events").Result;
            eventsList = response.Content.ReadAsAsync<IEnumerable<EVENTO>>().Result;

            IEnumerable<USUARIO> usersList;
            response = GlobalVariables.WebApiClient.GetAsync("users").Result;
            usersList = response.Content.ReadAsAsync<IEnumerable<USUARIO>>().Result;

            SubsViewModel subsViewModel = new SubsViewModel();
            subsViewModel.InscricaoVM = subsList;
            subsViewModel.EventoVM = eventsList;
            subsViewModel.UsuarioVM = usersList;

            return View(subsViewModel);
        }

        public ActionResult ValidateSub(int id = 0)
        {
            List<INSCRICAO> subsList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("subs").Result;
            subsList = response.Content.ReadAsAsync<List<INSCRICAO>>().Result;

            var subDetails = subsList.Where(x => x.COD_INSCRICAO == id).FirstOrDefault();

            List<EVENTO> eventsList;
            response = GlobalVariables.WebApiClient.GetAsync("events").Result;
            eventsList = response.Content.ReadAsAsync<List<EVENTO>>().Result;

            var eventDetails = eventsList.Where(x => x.COD_EVENTO == subDetails.COD_EVENTO).FirstOrDefault();

            subDetails.DATA_HORA_PARTICIPACAO = DateTime.Now;
            subDetails.COD_VALIDADO = 1;

            response = GlobalVariables.WebApiClient.PutAsJsonAsync("subs/" + subDetails.COD_INSCRICAO.ToString(), subDetails).Result;
            TempData["Success"] = "Usuário validado com sucesso.";

            return RedirectToAction("Validate");
        }

        public ActionResult SubsList()
        {
            List<INSCRICAO> subsList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("subs").Result;
            subsList = response.Content.ReadAsAsync<List<INSCRICAO>>().Result;

            List<EVENTO> eventsList;
            response = GlobalVariables.WebApiClient.GetAsync("events").Result;
            eventsList = response.Content.ReadAsAsync<List<EVENTO>>().Result;

            List<USUARIO> usersList;
            response = GlobalVariables.WebApiClient.GetAsync("users").Result;
            usersList = response.Content.ReadAsAsync<List<USUARIO>>().Result;

            SubsViewModel subsViewModel = new SubsViewModel();
            subsViewModel.InscricaoVM = subsList;
            subsViewModel.EventoVM = eventsList;
            subsViewModel.UsuarioVM = usersList;

            return View(subsViewModel);
        }
    }
}