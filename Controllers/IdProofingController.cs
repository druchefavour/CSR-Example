using Ibis_CSR_Tool.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
    public class IdProofingController : ControllerBase
    {
        private readonly ILogger<IdProofingController> _logger;
        private readonly IConfiguration _configuration;
        public IdProofingController(ILogger<IdProofingController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("IdProofParameters")]
        public object IdProofUser(object data, string result, string _result, string API_Key)
        {
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";
            IdProofUser obj = System.Text.Json.JsonSerializer.Deserialize<IdProofUser>(data.ToString());
            if ((obj != null) &&
                ((!String.IsNullOrEmpty(obj.firstName)) || (!String.IsNullOrEmpty(obj.lastName)) ||
                (!String.IsNullOrEmpty(obj.login)) || (!String.IsNullOrEmpty(obj.email)) ||
                (!String.IsNullOrEmpty(obj.city)) || (!String.IsNullOrEmpty(obj.state)) ||
                (!String.IsNullOrEmpty(obj.drivers_license)) || (!String.IsNullOrEmpty(obj.dateofbirth)) ||
                (!String.IsNullOrEmpty(obj.ibis_override)) ||
                (!String.IsNullOrEmpty(obj.idproofing_LOA)) || (!String.IsNullOrEmpty(obj.idproofing_score)) ||
                (!String.IsNullOrEmpty(obj.idproofing_status)) || (!String.IsNullOrEmpty(obj.mobilePhone)) ||
                (!String.IsNullOrEmpty(obj.primaryPhone)) || (!String.IsNullOrEmpty(obj.SSN)) ||
                (!String.IsNullOrEmpty(obj.answer)) || (!String.IsNullOrEmpty(obj.question)) ||
                (!String.IsNullOrEmpty(obj.zipCode)) || (!String.IsNullOrEmpty(obj.ibisid))))
            {
                var uri = baseUrl + "/api/IdProofing/IdProofParameters";
               
                string strUserName = (!String.IsNullOrEmpty(obj.login) ? " and profile.login sw \"" + obj.login + "\"" : "");
                string strFirstName = (!String.IsNullOrEmpty(obj.firstName) ? " and profile.firstName sw \"" + obj.firstName + "\"" : "");
                string strLastName = (!String.IsNullOrEmpty(obj.lastName) ? " and profile.lastName sw \"" + obj.lastName + "\"" : "");
                string strEmail = (!String.IsNullOrEmpty(obj.email) ? " and profile.email sw \"" + obj.email + "\"" : "");
                string strCity = (!String.IsNullOrEmpty(obj.city) ? " and profile.email sw \"" + obj.city + "\"" : "");
                string strState = (!String.IsNullOrEmpty(obj.state) ? " and profile.email sw \"" + obj.state + "\"" : "");
                string strZip = (!String.IsNullOrEmpty(obj.zipCode) ? " and profile.email sw \"" + obj.zipCode + "\"" : "");
                string strIbisID = (!String.IsNullOrEmpty(obj.ibisid) ? " and profile.email sw \"" + obj.ibisid + "\"" : "");
                string strFinalSearchString = "?search=" + strUserName + strFirstName + strLastName + strEmail + strCity + strState + strZip + strIbisID + "&limit=10";
                strFinalSearchString = strFinalSearchString.Replace("= and ", "= ");

                using (var httpClient = new HttpClient())
                {

                    var req = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(uri)
                    };
                    /*------------------------------------------------------------*/
                }

                var OktaDomain = _configuration.GetSection("Okta").GetSection("OktaDomain").Value;
                API_Key = _configuration.GetSection("Okta").GetSection("API Key").Value;

                // Check for uniqueness of email:
                var _client = new RestClient(OktaDomain + "/api/v1/users?search=profile.email profile.email sw \"" + obj.email + "\"");
                _client.Timeout = -1;
                var _request = new RestRequest(Method.GET);
                _request.AddHeader("Accept", "application/json");
                _request.AddHeader("Content-Type", "application/json");
                _request.AddHeader("Authorization", "SSWS" + API_Key);
                _request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=C134E9F9F4AE1A1C765F2D26E0B9F2CE");
                IRestResponse _response = _client.Execute(_request);

                _result = _response.Content;

                IdProofUser idProofUser = new IdProofUser();
                idProofUser = JsonConvert.DeserializeObject<IdProofUser>(_result);
                // ----------------------------- //
                // Create and activate new user
                if (idProofUser.login == "" || idProofUser.login == null)
                {
                    var client = new RestClient(OktaDomain + "/api/v1/users/" + obj.Id);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.PUT);
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Authorization", "SSWS" + API_Key);
                    request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=A190134EC6B0FCC6D5960646CC8C8D17");
                    
                    var body = @"{" + "\n" +
                    @"    ""profile"": {" + "\n" +
                    @"        ""firstName"":""" + obj.firstName + '"' + "," + "\n" +
                    @"        ""lastName"": """ + obj.lastName + '"' + "," + "\n" +
                    @"        ""email"": """ + obj.email + '"' + "," + "\n" +
                    @"        ""login"":""" + obj.login + '"' + "," + "\n" +
                    @"        ""primaryPhone"":""" + obj.primaryPhone + '"' + "," + "\n" +
                    @"        ""birthdate"":""" + obj.dateofbirth + '"' + "," + "\n" +
                    @"        ""drivers_license"":""" + obj.drivers_license + '"' + "," + "\n" +
                    @"        ""idproofing_LOA"":""" + obj.idproofing_LOA + '"' + "," + "\n" +
                    @"        ""idproofing_score"":""" + obj.idproofing_score + '"' + "," + "\n" +
                    @"        ""idproofing_status"":""" + obj.idproofing_status + '"' + "," + "\n" +
                    @"        ""mobilePhone"":""" + obj.mobilePhone + '"' + "," + "\n" +
                    @"        ""SSN"":""" + obj.SSN + '"' + "," + "\n" +
                    @"        ""streetAddress"": """ + obj.streetAddress + '"' + "," + "\n" +
                    @"        ""ibisid"": """ + obj.ibisid + '"' + "," + "\n" +
                    @"        ""city"":  """ + obj.city + '"' + "," + "\n" +
                    @"        ""state"": """ + obj.state + '"' + "," + "\n" +
                    @"        ""zipCode"": """ + obj.zipCode + '"' + "\n" +
                    @"  }" + "\n" +
                    @"}";
                    ///*---------------------------------------------------*/
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                result = response.Content;
              }
            }
            return result;
        }
    }
}
