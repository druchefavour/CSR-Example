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
    public class NullifyCustomController : ControllerBase
    {
        private readonly ILogger<NullifyCustomController> _logger;
        private readonly IConfiguration _configuration;

        public NullifyCustomController(ILogger<NullifyCustomController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        // Nullify Drivers License

        [HttpPost]
        [Route("nullifycustom")]
        public object NullifyDriversLicense(object data, string result, string API_Key)
        {
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";
            // Post User Id, Drivers License, BirthDate, email, login, firstName and lastName

            LinkUser obj = System.Text.Json.JsonSerializer.Deserialize<LinkUser>(data.ToString());
            if (obj != null)
            {
                var uri = baseUrl + "/api/NullifyCustom/nullifycustom";

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

                // Nullify DL/StateID and Birthdate
                var client = new RestClient(OktaDomain + "/api/v1/users/" + obj.Id);
                client.Timeout = -1;
                var request = new RestRequest(Method.PUT);
                request.AddHeader("Authorization", "SSWS" + API_Key);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Cookie", "DT=DI0V6ZxpjfZSe-PzN2WCDH0lw; JSESSIONID=A190134EC6B0FCC6D5960646CC8C8D17");
                var body = @"{" + "\n" +
                @"" + "\n" +
                @"""profile"": {" + "\n" +
                @"    ""firstName"": """ + obj.FirstName + '"' + "," + "\n" +
                @"    ""lastName"": """ + obj.LastName + '"' + "," + "\n" +
                @"    ""email"": """ + obj.Email + '"' + "," + "\n" +
                @"    ""login"": """ + obj.Login + '"' + "," + "\n" +
                @"    ""primaryPhone"":""" + obj.PrimaryPhone + '"' + "," + "\n" +
                @"    ""streetAddress"": """ + obj.StreetAddress + '"' + "," + "\n" +
                @"    ""city"":  """ + obj.City + '"' + "," + "\n" +
                @"    ""state"": """ + obj.State + '"' + "," + "\n" +
                @"    ""ibisid"": """ + obj.IbisID + '"' + "," + "\n" +
                @"    ""zipCode"": """ + obj.ZipCode + '"' + "," + "\n" +
                @"    ""birthdate"": """ + obj.BirthDate + '"' + "," + "\n" +
                @"    ""drivers_license"": """"" + "\n" +
                @"  }" + "\n" +
                @"}";

                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                result = response.Content;
              }
            return result;
        }
    }
}
