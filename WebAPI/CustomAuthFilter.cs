using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace WebAPI
{
    public class CustomAuthFilter : System.Web.Http.AuthorizeAttribute, IAuthenticationFilter
    {
        public bool AllowMultiple { get { return false; } }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            string authParameter = string.Empty;
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authenticationHeaderValue = request.Headers.Authorization;

            if (authenticationHeaderValue == null)
            {
                context.ErrorResult = new AuthFailureResult("Missing authorization header.", request);
                return;
            }
            else if (authenticationHeaderValue.Scheme != "Bearer")
            {
                context.ErrorResult = new AuthFailureResult("Invalid authorization schema.", request);
                return;
            }
            else if (String.IsNullOrEmpty(authenticationHeaderValue.Parameter))
            {
                context.ErrorResult = new AuthFailureResult("Missing token.", request);
                return;
            }

            context.Principal = TokenManager.GetPrincipal(authenticationHeaderValue.Parameter);
        }
        
        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var result = await context.Result.ExecuteAsync(cancellationToken);
            if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                result.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Basic", "realm=localhost"));
            }

            context.Result = new ResponseMessageResult(result);
        }
    }

    public class AuthFailureResult : IHttpActionResult
    {
        public string ReasonPhrase;
        public HttpRequestMessage Request { get; set; }

        public AuthFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            responseMessage.RequestMessage = Request;
            responseMessage.ReasonPhrase = ReasonPhrase;
            return responseMessage;
        }
    }
}