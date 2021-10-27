using Ibis_CSR_Tool.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ibis_CSR_Tool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkUserController : ControllerBase
    {
        private readonly ILogger<LinkUserController> _logger;
        private readonly IConfiguration _configuration;
        public LinkUserController(ILogger<LinkUserController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("getgroups")]
        public string[] GetGroups(string[] result)
        {
            //List Groups to get the Group IDs
            var client = new RestClient("https://dev-41732283.okta.com/api/v1/groups");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "SSWS 00ZqJN7_u8OgrxQhh12lKyx7hwopZ7cImh9GW-US1B");
            request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=8190536A901C68DE7B05089A0877A0B0; sid=102fDyn28baS3i9qoQwLyhqsg; t=default");
            IRestResponse response = client.Execute(request);

            var groupResponse = response.Content;

            List<GroupClass> groupData = JsonConvert.DeserializeObject<List<GroupClass>>(groupResponse.ToString());

            foreach (var c in groupData)
            {
                result = new string[] { c.id, c.profile.name };
            }

            var array = new List<GroupClass>().ToArray();
            foreach (var c in groupData)
            {
            }
            return result;
        }

        [HttpPost]
        [Route("linktoibis")]
        public object LinkUserToIbis(object data, string result, string _result, string ___result, string API_Key, string users)
        {
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";
            // Post Id, Ibis Request Group ID and IbisID

            LinkUser obj = System.Text.Json.JsonSerializer.Deserialize<LinkUser>(data.ToString());
            if (obj != null)
            {
                var uri = baseUrl + "/api/LinkUser/linktoibis";

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


                // Add User to Ibis Group
    
                var __client = new RestClient(OktaDomain + "/api/v1/groups/" + IbisGroupID + "/users/" + obj.Id);
                __client.Timeout = -1;
                var __request = new RestRequest(Method.PUT);
                __request.AddHeader("Accept", "application/json");
                __request.AddHeader("Content-Type", "application/json");
                __request.AddHeader("Authorization", "SSWS" + API_Key);
                __request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=A190134EC6B0FCC6D5960646CC8C8D17");
                var __body = @"";
                __request.AddParameter("application/json", __body, ParameterType.RequestBody);
                IRestResponse __response = __client.Execute(__request);
                result = __response.Content;

                // Get the groups of a user
                var client = new RestClient(OktaDomain + "/api/v1/users/" + obj.Id + "/groups");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "SSWS" + API_Key);
                request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=0DFE646B2DB13BBDCF1495D598D2F43E");
                IRestResponse response = client.Execute(request);
                // Post Ibis Acess Request group and Ibis Group Id

                // Post User Profile - login, email, firstname and lastname
                //Update Users profile in Okta with IBISID
                var _client = new RestClient(OktaDomain + "/api/v1/users/" + obj.Id);
                _client.Timeout = -1;
                var _request = new RestRequest(Method.PUT);
                _request.AddHeader("Authorization", "SSWS" + API_Key);
                _request.AddHeader("Content-Type", "application/json");
                _request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=A190134EC6B0FCC6D5960646CC8C8D17");
                var _body = @"{" + "\n" +
                @"" + "\n" +
                @"""profile"": {" + "\n" +
                @"    ""firstName"": """ + obj.FirstName + '"' + "," + "\n" +
                @"    ""lastName"": """ + obj.LastName + '"' + "," + "\n" +
                @"    ""email"": """ + obj.Email + '"' + "," + "\n" +
                @"    ""login"": """ + obj.Login + '"' + "," + "\n" +
                @"    ""ibisid"": """ + obj.IbisID + '"' + "," + "\n" +
                @"    ""drivers_license"": """ + obj.DriversLicense + '"' + "," + "\n" +
                @"    ""birthdate"": """ + obj.BirthDate + '"' +"," + "\n" +
                @"    ""primaryPhone"": """ + obj.PrimaryPhone + '"' + "," + "\n" +
                @"    ""streetAddress"": """ + obj.StreetAddress + '"' + "," + "\n" +
                @"    ""city"": """ + obj.City + '"' + "," + "\n" +
                @"    ""state"": """ + obj.State + '"' + "," + "\n" +
                @"    ""zipCode"": """ + obj.ZipCode + '"' + "\n" +
                @"  }" + "\n" +
                @"}";

                _request.AddParameter("application/json", _body, ParameterType.RequestBody);
                IRestResponse _response = _client.Execute(_request);

                users =_response.Content;

                var groupResponse = response.Content;

                List<GroupClass> groupData = JsonConvert.DeserializeObject<List<GroupClass>>(groupResponse.ToString());

                // Remove User from Ibis Access Request Group

                if (groupData[0].profile.name != null && groupData[0].profile.name == "IBIS Access Request")
                {
                    var ___client = new RestClient(OktaDomain + "/api/v1/groups/" + IbisAccessGroupID + " /users/ " + obj.Id);
                    ___client.Timeout = -1;
                    var ___request = new RestRequest(Method.DELETE);
                    ___request.AddHeader("Accept", "application/json");
                    ___request.AddHeader("Content-Type", "application/json");
                    ___request.AddHeader("Authorization", "SSWS" + API_Key);
                    ___request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=A190134EC6B0FCC6D5960646CC8C8D17");
                    var ___body = @"";
                    ___request.AddParameter("application/json", ___body, ParameterType.RequestBody);
                    IRestResponse ___response = ___client.Execute(___request);
                    ___result = ___response.Content;
                }
                else
                {
                    Console.WriteLine("Do Nothing");
                }

            }
            return users;
        }
    }
}
