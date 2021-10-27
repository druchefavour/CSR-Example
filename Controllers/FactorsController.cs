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
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Ibis_CSR_Tool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactorsController : ControllerBase
    {
        private readonly ILogger<FactorsController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public FactorsController(ILogger<FactorsController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("listfactors")]
        [HttpGet]
        [Route("factors")]
        public object ListFactor(object data, string result, string API_Key)
        {
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";
            LinkUser obj = System.Text.Json.JsonSerializer.Deserialize<LinkUser>(data.ToString());
            if (obj != null)
            {
                var uri = baseUrl + "/api/Factors/listfactors";

                using (var _httpClient = new HttpClient())
                {

                    var req = new HttpRequestMessage
                    {
                        Method = HttpMethod.Put,
                        RequestUri = new Uri(uri)
                    };
                }

                var OktaDomain = _configuration.GetSection("Okta").GetSection("OktaDomain").Value;
                API_Key = _configuration.GetSection("Okta").GetSection("API Key").Value;

                var client = new RestClient(OktaDomain + "/api/v1/users/" + obj.Id + "/factors");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization",  "SSWS" + API_Key);
                request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=A190134EC6B0FCC6D5960646CC8C8D17");
                IRestResponse response = client.Execute(request);

                result = response.Content;
            }
            return result;
        }

        [HttpPost]
        [Route("clearfactors")]
        public object ClearFactor(object data, string _4result,  string _result, string __result, string ___result, string API_Key)
        {
            // UserId from the frontend

            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";

            MFAFactor obj = System.Text.Json.JsonSerializer.Deserialize<MFAFactor>(data.ToString());
            if (obj != null)
            {
                var uri = baseUrl + "/api/Factors/clearfactors"; 

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

                /* Reset Factors one after the other */

                /* Reset SMS */
                if (!String.IsNullOrEmpty(obj.Idsms))
                {
                    var _client = new RestClient(OktaDomain + "/api/v1/users/" + obj.UserId + "/factors/" + obj.Idsms);
                    _client.Timeout = -1;
                    var _request = new RestRequest(Method.DELETE);
                    _request.AddHeader("Accept", "application/json");
                    _request.AddHeader("Content-Type", "application/json");
                    _request.AddHeader("Authorization", "SSWS 00ZqJN7_u8OgrxQhh12lKyx7hwopZ7cImh9GW-US1B");
                    _request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=2515524F7FFF6BE3C9121D05EF080CB0");
                    var _body = @"";
                    _request.AddParameter("application/json", _body, ParameterType.RequestBody);
                    IRestResponse _response = _client.Execute(_request);

                   _result = _response.Content;
                }
                if (!String.IsNullOrEmpty(obj.Idem))
                {
                    var __client = new RestClient(OktaDomain + "/api/v1/users/" + obj.UserId + "/factors/" + obj.Idem);
                    __client.Timeout = -1;
                    var __request = new RestRequest(Method.DELETE);
                    __request.AddHeader("Accept", "application/json");
                    __request.AddHeader("Content-Type", "application/json");
                    __request.AddHeader("Authorization", "SSWS 00ZqJN7_u8OgrxQhh12lKyx7hwopZ7cImh9GW-US1B");
                    __request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=2515524F7FFF6BE3C9121D05EF080CB0");
                    var __body = @"";
                    __request.AddParameter("application/json", __body, ParameterType.RequestBody);
                    IRestResponse __response = __client.Execute(__request);

                    __result = __response.Content;

                }
                if (!String.IsNullOrEmpty(obj.IdGA))
                {
                    var ___client = new RestClient(OktaDomain + "/api/v1/users/" + obj.UserId + "/factors/" + obj.IdGA);
                    ___client.Timeout = -1;
                    var ___request = new RestRequest(Method.DELETE);
                    ___request.AddHeader("Accept", "application/json");
                    ___request.AddHeader("Content-Type", "application/json");
                    ___request.AddHeader("Authorization", "SSWS 00ZqJN7_u8OgrxQhh12lKyx7hwopZ7cImh9GW-US1B");
                    ___request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=2515524F7FFF6BE3C9121D05EF080CB0");
                    var ___body = @"";
                    ___request.AddParameter("application/json", ___body, ParameterType.RequestBody);
                    IRestResponse ___response = ___client.Execute(___request);

                  ___result = ___response.Content;

                }
                if (!String.IsNullOrEmpty(obj.Idokver))
                {
                    var _4client = new RestClient(OktaDomain + "/api/v1/users/" + obj.UserId + "/factors/" + obj.Idokver);
                    _4client.Timeout = -1;
                    var _4request = new RestRequest(Method.DELETE);
                    _4request.AddHeader("Accept", "application/json");
                    _4request.AddHeader("Content-Type", "application/json");
                    _4request.AddHeader("Authorization", "SSWS 00ZqJN7_u8OgrxQhh12lKyx7hwopZ7cImh9GW-US1B");
                    _4request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=2515524F7FFF6BE3C9121D05EF080CB0");
                    var _4body = @"";
                    _4request.AddParameter("application/json", _4body, ParameterType.RequestBody);
                    IRestResponse _4response = _4client.Execute(_4request);
                    _4result = _4response.Content;
                }
            }
            return new string[] {_result, __result,___result, _4result};
            }
        }
     }
