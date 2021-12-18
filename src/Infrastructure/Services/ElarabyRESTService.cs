using RestSharp;
using System.Linq;
using HtmlAgilityPack;

namespace ElarabyCA.Infrastructure.Services
{
    public class ElarabyRESTService
    {
        public ElarabyRESTAuthentication Authentication { get; set; }

        private IRestResponse Authenticate(IRestResponse loginPage = null)
        {
            RestClient client;
            RestRequest request;
            IRestResponse response;

            if (loginPage == null)
            {
                //client = new RestClient("http://elarabyascportal.azurewebsites.net/Account/Login");
                client = new RestClient("https://elarabyportalpfs.powerappsportals.com/en-US/SignIn");
                client.Timeout = -1;
                request = new RestRequest(Method.GET);
                //request.AddHeader("Proxy-Connection", "keep-alive");
                //request.AddHeader("Cache-Control", "max-age=0");
                //request.AddHeader("Upgrade-Insecure-Requests", "1");
                //request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.117 Safari/537.36");
                request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.AddHeader("Accept-Encoding", "gzip, deflate");
                request.AddHeader("Accept-Language", "en-US,en;q=0.9");
                response = client.Execute(request);
                loginPage = response;
            }

            string username = "2100001197";
            string password = "123456";
            //string tokenCookie = loginPage.Cookies[0].Value;
            var tokenParameter = loginPage.GetRequestVerificationToken();

            client = new RestClient("https://elarabyportalpfs.powerappsportals.com/en-US/SignIn?ReturnUrl=%2Fen-US%2FOrders%2F");
            client.Timeout = -1;
            request = new RestRequest(Method.POST);
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Cache-Control", "max-age=0");
            request.AddHeader("Upgrade-Insecure-Requests", "1");
            request.AddHeader("Origin", "https://elarabyportalpfs.powerappsportals.com");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36");
            request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            request.AddHeader("Sec-Fetch-Site", "same-origin");
            request.AddHeader("Sec-Fetch-Mode", "navigate");
            request.AddHeader("Sec-Fetch-User", "?1");
            request.AddHeader("Sec-Fetch-Dest", "document");
            request.AddHeader("Referer", "https://elarabyportalpfs.powerappsportals.com/en-US/SignIn?ReturnUrl=%2Fen-US%2FOrders%2F");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Accept-Language", "en-US,en;q=0.9");

           var Dynamics365PortalAnalytics = loginPage.Cookies.First(x => x.Name == "Dynamics365PortalAnalytics");
           var ASPNET_SessionId = loginPage.Cookies.First(x => x.Name == "ASP.NET_SessionId");
           var __RequestVerificationToken = loginPage.Cookies.First(x => x.Name == "__RequestVerificationToken");
           var ARRAffinity = loginPage.Cookies.First(x => x.Name == "ARRAffinity");

           var cookieString = string.Format("Dynamics365PortalAnalytics={0}; ASP.NET_SessionId={1}; __RequestVerificationToken={2}; " +
                "ARRAffinity={3}; timezoneoffset=-120; isDSTSupport=false; isDSTObserved=false; " +
                "ContextLanguageCode=en-US; timeZoneCode=140", Dynamics365PortalAnalytics.Value, ASPNET_SessionId.Value, 
                    __RequestVerificationToken.Value, ARRAffinity.Value);
            request.AddHeader("Cookie", cookieString);

            request.AddParameter("__RequestVerificationToken", tokenParameter);
            request.AddParameter("Username", username);
            request.AddParameter("Password", password);
            request.AddParameter("RememberMe", "true");
            response = client.Execute(request);

            Authentication = response.ExtractOrUpdateAuthentication();
            return response;
        }

        public IRestResponse NavigateToHomePage()
        {
            IRestResponse response;
            if (Authentication != null)
            {
                var client = new RestClient("http://elarabyascportal.azurewebsites.net/");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Proxy-Connection", "keep-alive");
                request.AddHeader("Cache-Control", "max-age=0");
                request.AddHeader("Upgrade-Insecure-Requests", "1");
                request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.117 Safari/537.36");
                request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.AddHeader("Accept-Encoding", "gzip, deflate");
                request.AddHeader("Accept-Language", "en-US,en;q=0.9");
                request.AddHeader("Cookie", $"__RequestVerificationToken={Authentication.VerificationToken}; " +
                        $"ASP.NET_SessionId={Authentication.SessionId}; " +
                        $".AspNet.ApplicationCookie={Authentication.ApplicationCookie}");
                response = client.Execute(request);
                if (response.IsRedirectLogin())
                    response = Authenticate(response);
            }
            else
                response = Authenticate();
            response.ExtractOrUpdateAuthentication(Authentication);
            return response;
        }

        public IRestResponse NavigateToWorkOrdersPage()
        {
            var client = new RestClient("http://elarabyascportal.azurewebsites.net/Orders?showGridOnly=true");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Upgrade-Insecure-Requests", "1");
            request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36");
            request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            request.AddHeader("Referer", "http://elarabyascportal.azurewebsites.net/");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Accept-Language", "en-US,en;q=0.9");
            request.AddHeader("Cookie", $"__RequestVerificationToken={Authentication.VerificationToken}; " +
                $"ASP.NET_SessionId={Authentication.SessionId}; " +
                $".AspNet.ApplicationCookie={Authentication.ApplicationCookie}");
            IRestResponse response = client.Execute(request);
            Authentication = response.ExtractOrUpdateAuthentication(Authentication);
            return response;
        }

        public IRestResponse NavigateToEditWorkOrderPage(string id)
        {
            var client = new RestClient("http://elarabyascportal.azurewebsites.net/Orders/Edit?WorkOrderId=" + id);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Upgrade-Insecure-Requests", "1");
            request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36");
            request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            request.AddHeader("Referer", "http://elarabyascportal.azurewebsites.net/");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Accept-Language", "en-US,en;q=0.9");
            request.AddHeader("Cookie", $"__RequestVerificationToken={Authentication.VerificationToken}; " +
                $"ASP.NET_SessionId={Authentication.SessionId}; " +
                $".AspNet.ApplicationCookie={Authentication.ApplicationCookie}");
            return client.Execute(request);
        }
    }

    public static class IRestResponseExtentions
    {
        public static string GetRequestVerificationToken(this IRestResponse response)
        {
            HtmlDocument _Doc = new HtmlDocument();
            _Doc.LoadHtml(response.Content);
            string Token = _Doc.DocumentNode
                .SelectSingleNode("//input[contains(@name,'__RequestVerificationToken')]")?
                .Attributes["value"]?.Value;
            return Token;
        }
        public static string GetElementByTagName(this IRestResponse response, string tagName)
        {
            HtmlDocument _Doc = new HtmlDocument();
            _Doc.LoadHtml("<div>" + response.Content + "</div>");
            var node = _Doc.DocumentNode.Descendants(tagName).FirstOrDefault();


            return node.OuterHtml;
        }
        public static ElarabyRESTAuthentication ExtractOrUpdateAuthentication(this IRestResponse response,
            ElarabyRESTAuthentication authentication = null)
        {
            var sessionId = response.Cookies.FirstOrDefault(x => x.Name == "ASP.NET_SessionId")?.Value;
            var applicationCookie = response.Cookies.FirstOrDefault(x => x.Name == ".AspNet.ApplicationCookie")?.Value;
            var verificationToken = response.GetRequestVerificationToken();
            return new ElarabyRESTAuthentication()
            {
                ApplicationCookie = applicationCookie ?? authentication?.ApplicationCookie,
                SessionId = sessionId ?? authentication?.SessionId,
                VerificationToken = verificationToken ?? authentication?.VerificationToken
            };
        }
        public static bool IsRedirectLogin(this IRestResponse response)
        {
            return response.StatusCode == System.Net.HttpStatusCode.OK &&
                    response.ResponseUri.AbsolutePath == "/Account/Login";
        }
    }

    public class ElarabyRESTAuthentication
    {
        public string ApplicationCookie { get; set; }
        public string SessionId { get; set; }
        public string VerificationToken { get; set; }
    }
}
