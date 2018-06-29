namespace VisBackendConsole
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class TokenController : ApiController
    {
        public static string Token;
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response = this.Request.CreateResponse(System.Net.HttpStatusCode.OK);
            response.Content = new StringContent(Token, System.Text.Encoding.UTF8, "text/plain");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            return response;
        }
    }
}
