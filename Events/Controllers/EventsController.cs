using Events.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Events.Controllers
{
    [AllowAnonymous]
    public class EventsController : Controller
    {
        public ActionResult Index(int? page, string currentFilter, string searchString, string searchDate)
        {
            IEnumerable<EVENTO> eventList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("events").Result;
            eventList = response.Content.ReadAsAsync<IEnumerable<EVENTO>>().Result;
            if (searchString != null || searchDate != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                eventList = eventList.Where(x => x.TITULO.Contains(searchString)
                                            || x.DESCRICAO.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(searchDate))
            {
                eventList = eventList.Where(x => x.DATA.Equals(Convert.ToDateTime(searchDate)));
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(eventList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return View(new EVENTO());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("events/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<EVENTO>().Result);
            }
        }

        public ActionResult Subscribe(INSCRICAO iNSCRICAO, int id = 0)
        {
            var userAuth = (ClaimsIdentity)User.Identity;
            if (userAuth.IsAuthenticated)
            {
                var identity = userAuth.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault().Value;

                IEnumerable<INSCRICAO> subsList;
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("subs").Result;
                if (response.IsSuccessStatusCode)
                {                   
                    subsList = response.Content.ReadAsAsync<IEnumerable<INSCRICAO>>().Result;
                    var subExists = subsList.Where(x => x.COD_USUARIO == Convert.ToInt32(identity) && x.COD_EVENTO == id)
                                            .Any(x => x.COD_EVENTO == id && x.COD_USUARIO == Convert.ToInt32(identity));

                    if (subExists)
                    {
                        TempData["Error"] = "Você já está cadastrado nesse evento.";
                        return Redirect("/Events/Details/" + id);
                    }
                    else
                    {
                        iNSCRICAO.COD_USUARIO = Convert.ToInt32(identity);
                        iNSCRICAO.COD_EVENTO = id;

                        response = GlobalVariables.WebApiClient.PostAsJsonAsync("subs", iNSCRICAO).Result;
                        TempData["Success"] = "Inscrição realizada com sucesso!";
                        return RedirectToAction("MyEvents");
                    }
                }
                else
                {
                    TempData["Error"] = "Ocorreu um erro ao efetuar sua requisição.";
                    return Redirect("/Events/Details/" + id);
                }
            }
            else
            {
                TempData["Error"] = "Inicie a sessão para continuar.";
                return RedirectToAction("Login", "Users");
            }
        }

        //Not working yet, needs to be verified: Controller + View
        [Authorize(Roles = "1,2")]
        public ActionResult MyEvents()
        {
            List<INSCRICAO> subsList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("subs").Result;
            subsList = response.Content.ReadAsAsync<List<INSCRICAO>>().Result;
            
            List<EVENTO> eventsList;
            response = GlobalVariables.WebApiClient.GetAsync("events").Result;
            eventsList = response.Content.ReadAsAsync<List<EVENTO>>().Result;

            SubsViewModel subsViewModel = new SubsViewModel();
            subsViewModel.InscricaoVM = subsList;
            subsViewModel.EventoVM = eventsList;

            return View(subsViewModel);
        }
        
        public ActionResult DeleteSub(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("subs/" + id.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Inscrição cancelada com sucesso.";
                return RedirectToAction("MyEvents", "Events");
            }
            else
            {
                TempData["Error"] = "Ocorreu um erro ao enviar sua requisição.";
                return RedirectToAction("MyEvents", "Events");
            }
        }
    }
}