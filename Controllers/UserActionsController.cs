using Ibis_CSR_Tool.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Okta.Sdk;
using Okta.Sdk.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ibis_CSR_Tool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserActionsController : ControllerBase
    {
        private readonly ILogger<UserActionsController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public UserActionsController(ILogger<UserActionsController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("resetpassword")]
        public object PostRestPasswordId(object data, string result, string API_Key)
        {
            UserActions obj = System.Text.Json.JsonSerializer.Deserialize<UserActions>(data.ToString());
            if (obj != null)
            {
                var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";

                var uri = baseUrl + "/api/userActions/resetpassword";

                using (var httpClient = new HttpClient())
                {

                    var req = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(uri)
                    };

                    Console.Write (obj.id);

                }

                Console.Write (obj.id);
                var OktaDomain = _configuration.GetSection("Okta").GetSection("OktaDomain").Value;
                API_Key = _configuration.GetSection("Okta").GetSection("API Key").Value;

                var client = new RestClient(OktaDomain + "/api/v1/users/" + obj.id + "/lifecycle/reset_password?sendEmail=true");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Authorization", "SSWS" + API_Key);
                request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=AB6F4FC0E042F1D6B572EBD2851DF2D9; sid=102fDyn28baS3i9qoQwLyhqsg; t=default");
                var body = @"";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);


                /*var _Client = new OktaClient(new OktaClientConfiguration
                {
                    OktaDomain = "https://dev-41732283.okta.com",
                    Token = "00ZqJN7_u8OgrxQhh12lKyx7hwopZ7cImh9GW-US1B"
                });

                var user = await _Client.Users.GetUserAsync(obj.id);
                await _Client.PostAsync(new Okta.Sdk.HttpRequest
                {
                    Uri = $"/api/v1/users/" + obj.id + "/lifecycle/reset_password",
                    PathParameters = new Dictionary<string, object>()
                    {
                        ["userId"] = user.Id,
                    },
                    QueryParameters = new Dictionary<string, object>()
                    {
                        ["sendEmail"] = true,
                    }
                });
            }*/
                return response.Content;
            }
            return "Ok! Password Reset is Sussessful";
        }

        [HttpPost]
        [Route("sendActivationEmail")]
        public object PostSendActivationEmail(object data, string result, string API_Key)
        {
            UserActions obj = System.Text.Json.JsonSerializer.Deserialize<UserActions>(data.ToString());

            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";
            var uri = baseUrl + "/api/userActions/sendActivationEmail";
            using (var httpClient = new HttpClient())
            {
                var req = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(uri)
                };
            }

            var OktaDomain = _configuration.GetSection("Okta").GetSection("OktaDomain").Value;
                API_Key = _configuration.GetSection("Okta").GetSection("API Key").Value;

            var client = new RestClient(OktaDomain + "/api/v1/users/" + obj.id + "/lifecycle/activate?sendEmail=true");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "SSWS " + API_Key);
            request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=C4EA68D0823154A60CE5C6B00959A6D0");
            var body = @"";
            request.AddParameter("application/json", body,  ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return response.Content;
        }

        [HttpPost]
        [Route("resendActivationEmail")]
        public object PostResendActivationEmail(object data, string result, string API_Key)
        {
            UserActions obj = System.Text.Json.JsonSerializer.Deserialize<UserActions>(data.ToString());

            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";
            var uri = baseUrl + "/api/userActions/resendActivationEmail";
            using (var httpClient = new HttpClient())
            {
                var req = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(uri)
                };
            }

            var OktaDomain = _configuration.GetSection("Okta").GetSection("OktaDomain").Value;
                API_Key = _configuration.GetSection("Okta").GetSection("API Key").Value;

            var client = new RestClient(OktaDomain + "/api/v1/users/" + obj.id + "/lifecycle/reactivate?sendEmail=true");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "SSWS " + API_Key);
            request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=C4EA68D0823154A60CE5C6B00959A6D0");
            var body = @"";
            request.AddParameter("application/json", body,  ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return response.Content;
        }

        [HttpPost]
        [Route("setpassworddirectly")]
        public object SetPasswordDirectly(object data, string result, string API_Key)
        {

            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";

            UserActions obj = System.Text.Json.JsonSerializer.Deserialize<UserActions>(data.ToString());
            if (obj != null)
            {
                var uri = baseUrl + "/api/userActions/setpassworddirectly";

                using (var httpClient = new HttpClient())
                {

                    var req = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(uri)
                    };

        }

                var OktaDomain = _configuration.GetSection("Okta").GetSection("OktaDomain").Value;
                API_Key = _configuration.GetSection("Okta").GetSection("API Key").Value;

                var client = new RestClient(OktaDomain + "/api/v1/users/" + obj.id);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "SSWS" + API_Key);
                request.AddHeader("Content-Type", "text/plain");
                request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=2EB7269852DB19253FC44684079E9969; sid=102fDyn28baS3i9qoQwLyhqsg; t=default");
                var body = @" ""credentials"": {" + "\n" +
                @"    ""password"" : { ""value"": """+ obj.password +'"'+ "}" + "\n" +
                @"  }";
                request.AddParameter("text/plain", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                result = response.Content;

                 }
                    return result;
               }

            }
        }

