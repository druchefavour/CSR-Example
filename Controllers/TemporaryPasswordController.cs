using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ibis_CSR_Tool.Models;
using RestSharp;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace Ibis_CSR_Tool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemporaryPasswordController : ControllerBase
    {
        private readonly ILogger<TemporaryPasswordController> _logger;
        private readonly IConfiguration _configuration;

        public TemporaryPasswordController(ILogger<TemporaryPasswordController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("TemporaryPassword")]
        public object TemporaryPasswordSet(object data, string result, string API_Key)
        {
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";

            UserId obj = System.Text.Json.JsonSerializer.Deserialize<UserId>(data.ToString());

            if (obj != null)
            {
                var uri = baseUrl + "/api/TemporaryPassword/TemporaryPassword";

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

                var client = new RestClient(OktaDomain + "/api/v1/users/" + obj.Id + "/lifecycle/expire_password?tempPassword=true");
         
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Authorization", "SSWS" + API_Key);
                request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=64FEE21B32658D995741C83A063C31A3; sid=102fDyn28baS3i9qoQwLyhqsg; t=default");
                var body = @"";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                result = response.Content;
            }
            return result;
        }

        [HttpPost]
        [Route("UnlockUser")]
        public object UnlockUser(object data, string API_Key, string result)
        {
            UserId obj = System.Text.Json.JsonSerializer.Deserialize<UserId>(data.ToString());

            if (obj != null)
            {
                var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";

                var uri = baseUrl + "/api/TemporaryPassword/UnlockUser";

                using (var httpClient = new HttpClient())
                {

                    var req = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(uri)
                    };

                }
            }

            var OktaDomain = _configuration.GetSection("Okta").GetSection("OktaDomain").Value;
            API_Key = _configuration.GetSection("Okta").GetSection("API Key").Value;

            var client = new RestClient(OktaDomain + "/api/v1/users/" + obj.Id + "/lifecycle/unlock");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "SSWS" + API_Key);
            request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=2A67976360FD12379B7F9189CEA47AF9; sid=102fDyn28baS3i9qoQwLyhqsg; t=default");
            var body = @"";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            //Get User and return user response

            if(obj.Id != null) {
                var _client = new RestClient(OktaDomain + "/api/v1/users/" + obj.Id);
                _client.Timeout = -1;
                var _request = new RestRequest(Method.GET);
                _request.AddHeader("Accept", "application/json");
                _request.AddHeader("Content-Type", "application/json");
                _request.AddHeader("Authorization", "SSWS" + API_Key);
                _request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=FF08EE9E689C0D0C7D4881AC6EAE71EF");
                IRestResponse _response = _client.Execute(_request);
                result = _response.Content;

            }
            return result;
        }
        
        [HttpPost]
        [Route("Answer")]
        public object RecoveryAnswer(object data, string API_Key)
        {
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";

            UserId obj = System.Text.Json.JsonSerializer.Deserialize<UserId>(data.ToString());

            if (obj != null)
            {
                var uri = baseUrl + "/api/TemporaryPassword/Answer";

                using (var httpClient = new HttpClient())
                {

                    var req = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(uri)
                    };

                }
            }

            var OktaDomain = _configuration.GetSection("Okta").GetSection("OktaDomain").Value;
            API_Key = _configuration.GetSection("Okta").GetSection("API Key").Value;
            Guid Password = Guid.NewGuid();

            var client = new RestClient(OktaDomain + "/api/v1/users/" + obj.Id + "/credentials/forgot_password?sendEmail=false");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "SSWS" + API_Key);
            request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=FF08EE9E689C0D0C7D4881AC6EAE71EF");
            var body = @"{" + "\n" +
            @"  ""password"": { ""value"": """ + Password + '"' + "\n" +
            @"        }," + "\n" +
            @"    ""recovery_question"": {" + "\n" +
                    @"            ""question"":  """ + obj.Question + '"' + "," + "\n" +
                    @"            ""answer"": """ + obj.Answer + '"' + "\n" +
                    @"        }" + "\n" +
            @"}";
            request.AddParameter("application/json", body,  ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
           
           if (response.Content.Contains("errorCode")) {
                return "User Not Verified";
           } else {
              return  response.Content;
           }
        } 
    }
}