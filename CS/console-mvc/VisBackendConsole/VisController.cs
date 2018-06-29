namespace VisBackendConsole
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class VisController : ApiController
    {
        private static readonly string SaveFile = @".\vis.json";

        public async Task<HttpResponseMessage> Post()
        {
            using (FileStream fs = new FileStream(SaveFile, FileMode.Create))
            {
                Stream input = await this.Request.Content.ReadAsStreamAsync();
                input.CopyTo(fs);
            }

            HttpResponseMessage response = this.Request.CreateResponse(System.Net.HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            return response;
        }

        public HttpResponseMessage Get()
        {
            HttpResponseMessage response = this.Request.CreateResponse(System.Net.HttpStatusCode.OK);
            response.Content = new StringContent(File.ReadAllText(SaveFile), System.Text.Encoding.UTF8, "application/json");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            return response;
        }
    }
}
