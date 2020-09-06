using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class LoginController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage ValidLogin(string email, string password, USUARIO uSUARIO)
        {
            if (email == uSUARIO.EMAIL && password == uSUARIO.SENHA)
            {
                return Request.CreateResponse(HttpStatusCode.OK, TokenManager.GenerateToken(email));
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, "Email ou senha inválidos.");
            }
        }

        [HttpGet]
        [CustomAuthFilter]
        public HttpResponseMessage GetUSUARIO()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Validado com sucesso.");
        }
    }
}
