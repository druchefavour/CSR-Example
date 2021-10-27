using Ibis_CSR_Tool.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
    public class SuspendUserController : ControllerBase
    {
        private readonly ILogger<SuspendUserController> _logger;
        private readonly IConfiguration _configuration;

        public SuspendUserController(ILogger<SuspendUserController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("suspenduser")]
        public object SuspendUser(object data, string result, string API_Key)
        {
            // Post Id, Ibis Request Group ID and IbisID

            LinkUser obj = System.Text.Json.JsonSerializer.Deserialize<LinkUser>(data.ToString());
            if (obj != null)
            {
                var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";

                var uri = baseUrl + "/api/SuspendUser/suspenduser";

                using (var httpClient = new HttpClient())
                {
                    var req = new HttpRequestMessage
                    {
                        Method = HttpMethod.Put,
                        RequestUri = new Uri(uri)
                    };
                }
                var OktaDomain = _configuration.GetSection("Okta").GetSection("OktaDomain").Value;
                API_Key = _configuration.GetSection("Okta").GetSection("API Key").Value;
                
                /* Check if Unlink is completed */
                if ((obj != null) &&
                  String.IsNullOrEmpty(obj.IbisID))
                {
                    var client = new RestClient(OktaDomain + "/api/v1/users/" + obj.Id + "/lifecycle/suspend");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("Authorization", "SSWS" + API_Key);
                    request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=A190134EC6B0FCC6D5960646CC8C8D17");
                    var body = @"";
                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                

                    if(obj.Id != null) {
                        //Get User and return user response
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
                
            }
            }
            return result;
        }

        [HttpPost]
        [Route("unsuspenduser")]
        public object UnSuspendUser(object data, string results, string API_Key)
        {
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";
            // Post Id, Ibis Request Group ID and IbisID

            LinkUser obj = System.Text.Json.JsonSerializer.Deserialize<LinkUser>(data.ToString());
            if (obj != null)
            {
                var uri = baseUrl + "/api/SuspendUser/unsuspenduser";

                using (var httpClient = new HttpClient())
                {
                    var req = new HttpRequestMessage
                    {
                        Method = HttpMethod.Put,
                        RequestUri = new Uri(uri)
                    };
                }

                var OktaDomain = _configuration.GetSection("Okta").GetSection("OktaDomain").Value;
                API_Key = _configuration.GetSection("Okta").GetSection("API Key").Value;
               
                var client = new RestClient(OktaDomain + "/api/v1/users/" + obj.Id + "/lifecycle/unsuspend");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Authorization", "SSWS" + API_Key);
                request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=A190134EC6B0FCC6D5960646CC8C8D17");
                var body = @"";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
            
                //Get User and return user response
                 if (obj.Id != null) {
                    var _client = new RestClient(OktaDomain + "/api/v1/users/" + obj.Id);
                    _client.Timeout = -1;
                    var _request = new RestRequest(Method.GET);
                    _request.AddHeader("Accept", "application/json");
                    _request.AddHeader("Content-Type", "application/json");
                    _request.AddHeader("Authorization", "SSWS" + API_Key);
                    _request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=FF08EE9E689C0D0C7D4881AC6EAE71EF");
                    IRestResponse _response = _client.Execute(_request);
                    results = _response.Content;
                }
            }
            Console.WriteLine(results);
           return results;
        }

    }
}

