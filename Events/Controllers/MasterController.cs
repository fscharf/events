using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;
using Events.Models;
using PagedList;


namespace Events.Controllers
{
  [Authorize(Roles = "3")]
  public class MasterController : Controller
  {
    // GET: Master
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult UsersList(int? page, string currentFilter, string searchString)
    {
      IEnumerable<USUARIO> userList;
      HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("users").Result;
      userList = response.Content.ReadAsAsync<IEnumerable<USUARIO>>().Result;

      if (searchString != null)
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
        userList = userList.Where(x => x.NOME.Contains(searchString)
                                  || x.EMAIL.Contains(searchString)
                                  || x.CELULAR.Contains(searchString));
      }
      int pageSize = 10;
      int pageNumber = (page ?? 1);

      return View(userList.ToPagedList(pageNumber, pageSize));
    }

    public ActionResult UserDetails(int id = 0)
    {
      if (id == 0)
      {
        return View(new USUARIO());
      }
      else
      {
        HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("users/" + id.ToString()).Result;
        return View(response.Content.ReadAsAsync<USUARIO>().Result);
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult UserDetails(USUARIO uSUARIO, int id = 0)
    {
      if (id == 0)
      {
        TempData["Error"] = "Usuário não localizado.";
        return View(uSUARIO);
      }
      else
      {
        HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("users/" + id.ToString(), uSUARIO).Result;
        if (response.IsSuccessStatusCode)
        {
          TempData["Success"] = "Dados atualizados com sucesso!";
          return RedirectToAction("UsersList", "Master");
        }
        else
        {
          TempData["Error"] = "Ocorreu um erro inesperado.";
          return View(uSUARIO);
        }
      }
    }

    public ActionResult UserCreate() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult UserCreate(USUARIO uSUARIO)
    {
      HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("users", uSUARIO).Result;
      if (response.IsSuccessStatusCode)
      {
        TempData["Success"] = "Usuário criado com sucesso.";
        return RedirectToAction("UsersList", "Master");
      }
      else
      {
        TempData["Error"] = "Ocorreu um erro ao enviar os dados para API.";
        return View("UserCreate", uSUARIO);
      }
    }

    public ActionResult UserDelete(int id)
    {
      HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("users/" + id.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        TempData["Success"] = "Dados atualizados com sucesso.";
        return RedirectToAction("UsersList", "Master");
      }
      else
      {
        TempData["Error"] = "Ocorreu um erro ao enviar sua requisição.";
        return View();
      }
    }
  }
}