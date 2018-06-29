using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisBackendConsole
{
    using Microsoft.IdentityModel.Clients.ActiveDirectory;
    using System.Threading;
    using System.Web.Http;
    using System.Web.Http.SelfHost;
    class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost:8080");

            config.Routes.MapHttpRoute(
                "API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            Task.Factory.StartNew(() =>
            {
                var context = new AuthenticationContext("https://login.microsoftonline.com/41173aa9-90ef-47d4-b807-8d29f9bb1cf3/oauth2/authorize", false);
                while (true)
                {
                    var result = context.AcquireTokenAsync(
                        "https://mengdongy2.crm5.dynamics.com/",
                        "4ce1ce40-4283-445a-8350-ee18f3af6579",
                        new Uri("urn:ietf:wg:oauth:2.0:oob"),
                        new PlatformParameters(PromptBehavior.Auto), new UserIdentifier("mengdongy@mengdongy.onmicrosoft.com", UserIdentifierType.RequiredDisplayableId)).Result;
                    TokenController.Token = result.AccessToken;
                    Thread.Sleep(60000);
                }
            });

            using (HttpSelfHostServer server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }
}
