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
    public class UnlinkUserController : ControllerBase
    {
        private readonly ILogger<UnlinkUserController> _logger;
        private readonly IConfiguration _configuration;

        public UnlinkUserController(ILogger<UnlinkUserController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("unlinkfromibis")]
        public object UnLinkUserFromIbis(object data, string result, string API_Key)
        {
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";

            /* Post Packet containing UserId, FirstName, LastName, Email, Login, DriversLicense and IbisID 
            From the Frontend*/

            LinkUser obj = System.Text.Json.JsonSerializer.Deserialize<LinkUser>(data.ToString());

            if (obj != null)
            {
                var uri = baseUrl + "/api/UnlinkUser/unlinkuserfromibis";

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
                var IbisAccessGroupID = _configuration.GetSection("Okta").GetSection("IBISAccessGroupID").Value; ;
                var IbisGroupID = _configuration.GetSection("Okta").GetSection("IBISGroupID").Value;

                // Remove User from Ibis Group
         
                if (!String.IsNullOrEmpty(IbisGroupID))
                {
                    var client = new RestClient(OktaDomain + "/api/v1/groups/" + IbisGroupID + "/users/" + obj.Id);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.DELETE);
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Authorization", "SSWS" + API_Key);
                    request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=A190134EC6B0FCC6D5960646CC8C8D17");
                    var body = @"";
                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                }
                // Add User to Ibis Request Access Group
                var _client = new RestClient(OktaDomain + "/api/v1/groups/" + IbisAccessGroupID + "/users/" + obj.Id);
                _client.Timeout = -1;
                var _request = new RestRequest(Method.PUT);
                _request.AddHeader("Accept", "application/json");
                _request.AddHeader("Content-Type", "application/json");
                _request.AddHeader("Authorization", "SSWS" + API_Key);
                _request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=A190134EC6B0FCC6D5960646CC8C8D17");
                var _body = @"";
                _request.AddParameter("application/json", _body, ParameterType.RequestBody);
                IRestResponse _response = _client.Execute(_request);

                // Remove IbisID from Okta UD
                var __client = new RestClient(OktaDomain + "/api/v1/users/" + obj.Id);
                __client.Timeout = -1;
                var __request = new RestRequest(Method.PUT);
                __request.AddHeader("Authorization", "SSWS" + API_Key);
                __request.AddHeader("Content-Type", "application/json");
                __request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=A190134EC6B0FCC6D5960646CC8C8D17");
                var __body = @"{" + "\n" +
                @"" + "\n" +
                @"""profile"": {" + "\n" +
                @"    ""firstName"": """ + obj.FirstName + '"' + "," + "\n" +
                @"    ""lastName"": """ + obj.LastName + '"' + "," + "\n" +
                @"    ""email"": """ + obj.Email + '"' + "," + "\n" +
                @"    ""login"": """ + obj.Login + '"' + "," + "\n" +
                @"    ""streetAddress"": """ + obj.StreetAddress + '"' + "," + "\n" +
                @"    ""drivers_license"": """ + obj.DriversLicense + '"' + "," + "\n" +
                @"    ""birthdate"": """ + obj.BirthDate + '"' + "," + "\n" +
                @"    ""city"": """ + obj.City + '"' + "," + "\n" +
                @"    ""state"": """ + obj.State + '"' + "," + "\n" +
                @"    ""zipCode"": """ + obj.ZipCode + '"' + "," + "\n" +
                @"    ""primaryPhone"": """ + obj.PrimaryPhone + '"' + "," + "\n" +
                @"    ""ibisid"": """" " + "\n" +
                @"  } " + "\n" +
                @"}";
                __request.AddParameter("application/json", __body, ParameterType.RequestBody);
                IRestResponse __response = __client.Execute(__request);
                result = __response.Content;
            }

            return result;
        }
    }
}
