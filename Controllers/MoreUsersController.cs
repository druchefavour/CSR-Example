using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Ibis_CSR_Tool.Models;
using RestSharp;

namespace Ibis_CSR_Tool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoreUsersController : ControllerBase
    {
        private readonly ILogger<MoreUsersController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public bool AbsoluteUri { get; private set; }

        public MoreUsersController(ILogger<MoreUsersController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("SearchParameters")]
        
        /* --------------------------- New LoadMore ----------------------------------------------*/
        [EnableCors("AllowAll")]
        [HttpGet]
        [Route("SearchCasesTest")]
        //public async Task<List<ListUsers>> Post(object data, string result)
        public async Task<string []> Postmore (object data, string API_Key, string pg_1,string pg_2, string pg_3, string pg_4, 
            string pg_5, string pg_6, string pg_7,string pg_8, string pg_9, string pg_10,
            string urlNext, string _urlNext, string _2urlNext, string _3urlNext, 
            string _4urlNext, string _5urlNext, string _6urlNext, string _7urlNext, 
            string _8urlNext, string _9urlNext, string _10urlNext,
            string pg_1Url, string pg_2Url, string pg_3Url, string pg_4Url, string pg_5Url, 
            string pg_6Url, string pg_7Url, string pg_8Url, string pg_9Url, string pg_10Url)
        {
            IEnumerable<string> values;

            MyUsers usersData = new MyUsers();
            usersData.SearchUser = new List<ListUsers>();

            MyUsers _2usersData = new MyUsers();
            _2usersData.SearchUser = new List<ListUsers>();

            MyUsers _3usersData = new MyUsers();
            _3usersData.SearchUser = new List<ListUsers>();

            MyUsers _4usersData = new MyUsers();
            _4usersData.SearchUser = new List<ListUsers>();

            MyUsers _5usersData = new MyUsers();
            _5usersData.SearchUser = new List<ListUsers>();

            MyUsers _6usersData = new MyUsers();
            _6usersData.SearchUser = new List<ListUsers>();

            MyUsers _7usersData = new MyUsers();
            _7usersData.SearchUser = new List<ListUsers>();

            MyUsers _8usersData = new MyUsers();
            _8usersData.SearchUser = new List<ListUsers>();

            MyUsers _9usersData = new MyUsers();
            _9usersData.SearchUser = new List<ListUsers>();

            MyUsers _10usersData = new MyUsers();
            _10usersData.SearchUser = new List<ListUsers>();

            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";

            try
            {
                 Ibis_CSR_Tool.Models.SearchParameters obj = System.Text.Json.JsonSerializer.Deserialize<Ibis_CSR_Tool.Models.SearchParameters>(data.ToString());
                if ((obj != null) &&
                    ((!String.IsNullOrEmpty(obj.firstName)) || (!String.IsNullOrEmpty(obj.lastName)) ||
                    (!String.IsNullOrEmpty(obj.login)) || (!String.IsNullOrEmpty(obj.email)) ||
                    (!String.IsNullOrEmpty(obj.city)) || (!String.IsNullOrEmpty(obj.state)) ||
                    (!String.IsNullOrEmpty(obj.zipCode)) || (!String.IsNullOrEmpty(obj.ibisid))||
                    (!String.IsNullOrEmpty(obj.drivers_license))))
                {
                    var uri = baseUrl + "/api/moreUsers/SearchParameters";
                    string strUserName = (!String.IsNullOrEmpty(obj.login) ? " and profile.login sw \"" + obj.login + "\"" : "");
                    string strFirstName = (!String.IsNullOrEmpty(obj.firstName) ? " and profile.firstName sw \"" + obj.firstName + "\"" : "");
                    string strLastName = (!String.IsNullOrEmpty(obj.lastName) ? " and profile.lastName sw \"" + obj.lastName + "\"" : "");
                    string strEmail = (!String.IsNullOrEmpty(obj.email) ? " and profile.email sw \"" + obj.email + "\"" : "");
                    string strCity = (!String.IsNullOrEmpty(obj.city) ? " and profile.city sw \"" + obj.city + "\"" : "");
                    string strState = (!String.IsNullOrEmpty(obj.state) ? " and profile.state sw \"" + obj.state + "\"" : "");
                    string strZip = (!String.IsNullOrEmpty(obj.zipCode) ? " and profile.zipCode sw \"" + obj.zipCode + "\"" : "");
                    string strIbisID = (!String.IsNullOrEmpty(obj.ibisid) ? " and profile.ibisid sw \"" + obj.ibisid + "\"" : "");
                    string strdriversLicense = (!String.IsNullOrEmpty(obj.drivers_license) ? " and profile.drivers_license sw \"" + obj.drivers_license + "\"" : "");
                    string strFinalSearchString = "?search=" + strUserName + strFirstName + strLastName + strEmail + strCity + strState + strZip + strIbisID + strdriversLicense;
                    strFinalSearchString = strFinalSearchString.Replace("= and ", "= ");

                    using (var client = new HttpClient())
                    {

                        var request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            RequestUri = new Uri(uri)
                        };

                        var OktaDomain = _configuration.GetSection("Okta").GetSection("OktaDomain").Value;
                        API_Key = _configuration.GetSection("Okta").GetSection("API Key").Value;

                        pg_1Url = OktaDomain + "/api/v1/users";
                        pg_1Url += strFinalSearchString;

                        var httpClient = _httpClientFactory.CreateClient();

                        string __ContentType = "application/json";
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(__ContentType));
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SSWS", API_Key);
                      

                        var response = await httpClient.GetAsync(pg_1Url);

                        pg_1 = await response.Content.ReadAsStringAsync();

                        //string urlNext = "";
                        //////////////////////////////////////////
                        HttpHeaders headers = response.Headers;
                       // IEnumerable<string> values;
                        if (headers.TryGetValues("Link", out values))
                        {
                            if (values.Count() > 1)
                            {
                                urlNext = ((string[])values)[1];
                            }
                        }

                       
                        usersData.SearchUser = JsonConvert.DeserializeObject<List<ListUsers>>(pg_1);

                        /* --- Task2 --- */
                        if(!String.IsNullOrEmpty(urlNext)) {
                            usersData.PageLink = (!String.IsNullOrEmpty(urlNext)) ? (urlNext.Substring(1, urlNext.Length - (urlNext.Length - urlNext.IndexOf(';') + 2))) : "";
                            pg_2Url = usersData.PageLink;

                            HttpResponseMessage _httpResponseMessage = await httpClient.GetAsync(pg_2Url);
                            var _content = _httpResponseMessage.Content;
                            pg_2 = await _content.ReadAsStringAsync();
                            _2usersData.SearchUser = JsonConvert.DeserializeObject<List<ListUsers>>(pg_2);

                            HttpHeaders _headers = _httpResponseMessage.Headers;
                            if (_headers.TryGetValues("Link", out values))
                            {
                                if (values.Count() > 1)
                                {
                                    _urlNext = ((string[])values)[1];
                                }
                            }
                            } else
                            {
                                usersData.PageLink = "No More Page";
                                pg_2Url = "No More Page";

                                
                            }
                        
                         /* --- Task3 --- */
                       // var res_2 = usersData.SearchUser;
                        //httpClient.DefaultRequestHeaders.Add("LoadMore", _urlNext);
                       // IEnumerable<string> values;
                        if(!String.IsNullOrEmpty(_urlNext)) {
                       _2usersData.PageLink = _urlNext.Substring(1, _urlNext.Length - (_urlNext.Length - _urlNext.IndexOf(';') + 2));
                        pg_3Url = _2usersData.PageLink;

                         HttpResponseMessage _3httpResponseMessage = await httpClient.GetAsync(pg_3Url);
                        var _3content = _3httpResponseMessage.Content;
                        pg_3 = await _3content.ReadAsStringAsync();
                        
                        _3usersData.SearchUser = JsonConvert.DeserializeObject<List<ListUsers>>(pg_3);

                        HttpHeaders _3headers = _3httpResponseMessage.Headers;
                        if (_3headers.TryGetValues("Link", out values))
                        {
                            if (values.Count() > 1)
                            {
                                _3urlNext = ((string[])values)[1];
                            }
                        }
                        } else
                        {
                            _2usersData.PageLink = "No More Page";
                            pg_3Url = "No More Page";
                        }
                            /* --- Task4 --- */
                        if(!String.IsNullOrEmpty(_3urlNext)) {
                       _3usersData.PageLink = _3urlNext.Substring(1, _3urlNext.Length - (_3urlNext.Length - _3urlNext.IndexOf(';') + 2));
                        pg_4Url= _3usersData.PageLink;

                        HttpResponseMessage _4httpResponseMessage = await httpClient.GetAsync(pg_4Url);
                        var _4content = _4httpResponseMessage.Content;
                        pg_4 = await _4content.ReadAsStringAsync();
                        
                        _4usersData.SearchUser = JsonConvert.DeserializeObject<List<ListUsers>>(pg_4);

                        HttpHeaders _4headers = _4httpResponseMessage.Headers;
                        if (_4headers.TryGetValues("Link", out values))
                        {
                            if (values.Count() > 1)
                            {
                                _4urlNext = ((string[])values)[1];
                            }
                        }
                        } else
                        {
                            _3usersData.PageLink = "No More Page";
                            pg_4Url = "No More Page";
                        }

                          /* --- Task5 --- */
                        if(!String.IsNullOrEmpty(_4urlNext)) {
                       _4usersData.PageLink = _4urlNext.Substring(1, _4urlNext.Length - (_4urlNext.Length - _4urlNext.IndexOf(';') + 2));
                        pg_5Url= _4usersData.PageLink;

                        HttpResponseMessage _5httpResponseMessage = await httpClient.GetAsync(pg_5Url);
                        var _5content = _5httpResponseMessage.Content;
                        pg_5 = await _5content.ReadAsStringAsync();
                        
                        _5usersData.SearchUser = JsonConvert.DeserializeObject<List<ListUsers>>(pg_5);

                        HttpHeaders _5headers = _5httpResponseMessage.Headers;
                        if (_5headers.TryGetValues("Link", out values))
                        {
                            if (values.Count() > 1)
                            {
                                _5urlNext = ((string[])values)[1];
                            }
                        }
                        } else
                        {
                            _4usersData.PageLink = "No More Page";
                            pg_5Url = "No More Page";
                        }
                                 /* --- Task6 --- */
                        if(!String.IsNullOrEmpty(_5urlNext)) {
                       _5usersData.PageLink = _5urlNext.Substring(1, _5urlNext.Length - (_5urlNext.Length - _5urlNext.IndexOf(';') + 2));
                        pg_6Url= _5usersData.PageLink;

                        HttpResponseMessage _6httpResponseMessage = await httpClient.GetAsync(pg_6Url);
                        var _6content = _6httpResponseMessage.Content;
                        pg_6 = await _6content.ReadAsStringAsync();
                        
                        _6usersData.SearchUser = JsonConvert.DeserializeObject<List<ListUsers>>(pg_6);

                        HttpHeaders _6headers = _6httpResponseMessage.Headers;
                        if (_6headers.TryGetValues("Link", out values))
                        {
                            if (values.Count() > 1)
                            {
                                _6urlNext = ((string[])values)[1];
                            }
                        }
                        } else
                        {
                            _5usersData.PageLink = "No More Page";
                            pg_6Url = "No More Page";
                        }
                            /* --- Task7 --- */
                        if(!String.IsNullOrEmpty(_6urlNext)) {
                       _6usersData.PageLink = _6urlNext.Substring(1, _6urlNext.Length - (_6urlNext.Length - _6urlNext.IndexOf(';') + 2));
                        pg_7Url= _6usersData.PageLink;

                        HttpResponseMessage _7httpResponseMessage = await httpClient.GetAsync(pg_7Url);
                        var _7content = _7httpResponseMessage.Content;
                        pg_7 = await _7content.ReadAsStringAsync();
                        
                        _7usersData.SearchUser = JsonConvert.DeserializeObject<List<ListUsers>>(pg_7);

                        HttpHeaders _7headers = _7httpResponseMessage.Headers;
                        if (_7headers.TryGetValues("Link", out values))
                        {
                            if (values.Count() > 1)
                            {
                                _7urlNext = ((string[])values)[1];
                            }
                        }
                        } else
                        {
                            _6usersData.PageLink = "No More Page";
                            pg_7Url = "No More Page";
                        }

                       /* --- Task8 --- */
                        if(!String.IsNullOrEmpty(_7urlNext)) {
                       _7usersData.PageLink = _7urlNext.Substring(1, _7urlNext.Length - (_7urlNext.Length - _7urlNext.IndexOf(';') + 2));
                        pg_8Url= _7usersData.PageLink;

                        HttpResponseMessage _8httpResponseMessage = await httpClient.GetAsync(pg_8Url);
                        var _8content = _8httpResponseMessage.Content;
                        pg_8 = await _8content.ReadAsStringAsync();
                        
                        _8usersData.SearchUser = JsonConvert.DeserializeObject<List<ListUsers>>(pg_8);

                        HttpHeaders _8headers = _8httpResponseMessage.Headers;
                        if (_8headers.TryGetValues("Link", out values))
                        {
                            if (values.Count() > 1)
                            {
                                _8urlNext = ((string[])values)[1];
                            }
                        }
                        } else
                        {
                            _7usersData.PageLink = "No More Page";
                            pg_8Url = "No More Page";
                        }

                                    /* --- Task9 --- */
                        if(!String.IsNullOrEmpty(_8urlNext)) {
                       _8usersData.PageLink = _8urlNext.Substring(1, _8urlNext.Length - (_8urlNext.Length - _8urlNext.IndexOf(';') + 2));
                        pg_9Url= _8usersData.PageLink;

                        HttpResponseMessage _9httpResponseMessage = await httpClient.GetAsync(pg_9Url);
                        var _9content = _9httpResponseMessage.Content;
                        pg_9 = await _9content.ReadAsStringAsync();
                        
                        _9usersData.SearchUser = JsonConvert.DeserializeObject<List<ListUsers>>(pg_9);

                        HttpHeaders _9headers = _9httpResponseMessage.Headers;
                        if (_9headers.TryGetValues("Link", out values))
                        {
                            if (values.Count() > 1)
                            {
                                _9urlNext = ((string[])values)[1];
                            }
                        }
                        } else
                        {
                            _8usersData.PageLink = "No More Page";
                            pg_9Url = "No More Page";
                        }
                         /* --- Task10 --- */
                        if(!String.IsNullOrEmpty(_9urlNext)) {
                       _9usersData.PageLink = _9urlNext.Substring(1, _9urlNext.Length - (_9urlNext.Length - _9urlNext.IndexOf(';') + 2));
                        pg_10Url= _9usersData.PageLink;

                        HttpResponseMessage _10httpResponseMessage = await httpClient.GetAsync(pg_10Url);
                        var _10content = _10httpResponseMessage.Content;
                        pg_10 = await _10content.ReadAsStringAsync();
                        
                        _10usersData.SearchUser = JsonConvert.DeserializeObject<List<ListUsers>>(pg_10);

                        HttpHeaders _10headers = _10httpResponseMessage.Headers;
                        if (_10headers.TryGetValues("Link", out values))
                        {
                            if (values.Count() > 1)
                            {
                                _10urlNext = ((string[])values)[1];
                            }
                        }
                        } else
                        {
                            _10usersData.PageLink = "No More Page";
                            pg_10Url = "No More Page";
                        }
                    }
                }
                else
                {
                    /// Empty Search without any search parameters
                }
            }
            catch (AmbiguousMatchException)
            {
                throw;
            }

            return new string [] { pg_1, pg_2, pg_3, pg_4, pg_5, pg_6, pg_7, pg_8, pg_9, pg_10 };
        }
        /* -----------------------------------End New LoadMore-----------------------------------*/
        
        [EnableCors("AllowAll")]
        [HttpGet]
        [Route("Search")]
        public async Task<string> Post(object data, string result, string _result, string urlNext, string _urlNext, string API_Key)
        {
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";
            try
            {
                Ibis_CSR_Tool.Models.SearchParameters obj = System.Text.Json.JsonSerializer.Deserialize<Ibis_CSR_Tool.Models.SearchParameters>(data.ToString());
                if ((obj != null) &&
                    ((!String.IsNullOrEmpty(obj.firstName)) || (!String.IsNullOrEmpty(obj.lastName)) ||
                    (!String.IsNullOrEmpty(obj.login)) || (!String.IsNullOrEmpty(obj.email)) ||
                    (!String.IsNullOrEmpty(obj.city)) || (!String.IsNullOrEmpty(obj.state)) ||
                    (!String.IsNullOrEmpty(obj.zipCode)) || (!String.IsNullOrEmpty(obj.ibisid))||
                    (!String.IsNullOrEmpty(obj.drivers_license))))
                {
                    var uri = baseUrl + "/SearchUsers/SearchParameters";
                    string strUserName = (!String.IsNullOrEmpty(obj.login) ? " and profile.login sw \"" + obj.login + "\"" : "");
                    string strFirstName = (!String.IsNullOrEmpty(obj.firstName) ? " and profile.firstName sw \"" + obj.firstName + "\"" : "");
                    string strLastName = (!String.IsNullOrEmpty(obj.lastName) ? " and profile.lastName sw \"" + obj.lastName + "\"" : "");
                    string strEmail = (!String.IsNullOrEmpty(obj.email) ? " and profile.email sw \"" + obj.email + "\"" : "");
                    string strCity = (!String.IsNullOrEmpty(obj.city) ? " and profile.city sw \"" + obj.city + "\"" : "");
                    string strState = (!String.IsNullOrEmpty(obj.state) ? " and profile.state sw \"" + obj.state + "\"" : "");
                    string strZip = (!String.IsNullOrEmpty(obj.zipCode) ? " and profile.zipCode sw \"" + obj.zipCode + "\"" : "");
                    string strIbisID = (!String.IsNullOrEmpty(obj.ibisid) ? " and profile.ibisid sw \"" + obj.ibisid + "\"" : "");
                    string strdriversLicense = (!String.IsNullOrEmpty(obj.drivers_license) ? " and profile.drivers_license sw \"" + obj.drivers_license + "\"" : "");
                    string strFinalSearchString = "?search=" + strUserName + strFirstName + strLastName + strEmail + strCity + strState + strZip + strIbisID + strdriversLicense + "&limit=2";
                    strFinalSearchString = strFinalSearchString.Replace("= and ", "= ");

                    using (var client = new HttpClient())
                    {

                        var request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            RequestUri = new Uri(uri)
                        };

                        var OktaDomain = _configuration.GetSection("Okta").GetSection("OktaDomain").Value;
                        API_Key = _configuration.GetSection("Okta").GetSection("API Key").Value;

                        var Url_1 = OktaDomain + "/api/v1/users";
                        Url_1 += strFinalSearchString;

                        var httpClient = _httpClientFactory.CreateClient();

                        string __ContentType = "application/json";
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(__ContentType));
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SSWS", API_Key);
                      

                        var response = await httpClient.GetAsync(Url_1);

                        result = await response.Content.ReadAsStringAsync();

                        //string urlNext = "";
                        //////////////////////////////////////////
                        HttpHeaders headers = response.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("Link", out values))
                        {
                            if (values.Count() > 1)
                            {
                                urlNext = ((string[])values)[1];
                            }
                        }

                    }
                }
                else
                {
                    /// Empty Search without any search parameters
                }
                // 2nd Page

                MyUsers usersData = new MyUsers();
                usersData.SearchUser = new List<ListUsers>();
                usersData.SearchUser = JsonConvert.DeserializeObject<List<ListUsers>>(result);
                usersData.PageLink = (!String.IsNullOrEmpty(urlNext)) ? (urlNext.Substring(1, urlNext.Length - (urlNext.Length - urlNext.IndexOf(';') + 2))) : "";
                var res = usersData.SearchUser;

                var _httpClient = _httpClientFactory.CreateClient();

                string _ContentType = "application/json";
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_ContentType));
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SSWS", API_Key);

                if(!String.IsNullOrEmpty(usersData.PageLink)) {
                    var _response = await _httpClient.GetAsync(usersData.PageLink);
                    _result = await _response.Content.ReadAsStringAsync();
                } else
                {
                    Console.WriteLine("");
                }

            }
            catch (AmbiguousMatchException)
            {

                throw;
            }

            return _result;
        }

      

        [EnableCors("AllowAll")]
        [HttpGet]
        [Route("SearchCases")]
        //public async Task<List<ListUsers>> Post(object data, string result)
        public async Task<string> PostLoadmore(object data, string result, string _users, string urlNext, string _urlNext, string API_Key)
        {
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";
            try
            {
                Ibis_CSR_Tool.Models.SearchParameters obj = System.Text.Json.JsonSerializer.Deserialize< Ibis_CSR_Tool.Models.SearchParameters>(data.ToString());
                if ((obj != null) &&
                    ((!String.IsNullOrEmpty(obj.firstName)) || (!String.IsNullOrEmpty(obj.lastName)) ||
                    (!String.IsNullOrEmpty(obj.login)) || (!String.IsNullOrEmpty(obj.email)) ||
                    (!String.IsNullOrEmpty(obj.city)) || (!String.IsNullOrEmpty(obj.state)) ||
                    (!String.IsNullOrEmpty(obj.zipCode)) || (!String.IsNullOrEmpty(obj.ibisid))||
                    (!String.IsNullOrEmpty(obj.drivers_license))))
                {
                    var uri = baseUrl + "/SearchUsers/SearchParameters";
                    string strUserName = (!String.IsNullOrEmpty(obj.login) ? " and profile.login sw \"" + obj.login + "\"" : "");
                    string strFirstName = (!String.IsNullOrEmpty(obj.firstName) ? " and profile.firstName sw \"" + obj.firstName + "\"" : "");
                    string strLastName = (!String.IsNullOrEmpty(obj.lastName) ? " and profile.lastName sw \"" + obj.lastName + "\"" : "");
                    string strEmail = (!String.IsNullOrEmpty(obj.email) ? " and profile.email sw \"" + obj.email + "\"" : "");
                    string strCity = (!String.IsNullOrEmpty(obj.city) ? " and profile.city sw \"" + obj.city + "\"" : "");
                    string strState = (!String.IsNullOrEmpty(obj.state) ? " and profile.state sw \"" + obj.state + "\"" : "");
                    string strZip = (!String.IsNullOrEmpty(obj.zipCode) ? " and profile.zipCode sw \"" + obj.zipCode + "\"" : "");
                    string strIbisID = (!String.IsNullOrEmpty(obj.ibisid) ? " and profile.ibisid sw \"" + obj.ibisid + "\"" : "");
                    string strdriversLicense = (!String.IsNullOrEmpty(obj.drivers_license) ? " and profile.drivers_license sw \"" + obj.drivers_license + "\"" : "");
                    
                    string strFinalSearchString = "?search=" + strUserName + strFirstName + strLastName + strEmail + strCity + strState + strZip + strIbisID + strdriversLicense;
                    strFinalSearchString = strFinalSearchString.Replace("= and ", "= ");


                    var OktaDomain = _configuration.GetSection("Okta").GetSection("OktaDomain").Value;
                    API_Key = _configuration.GetSection("Okta").GetSection("API Key").Value;

                    var httpClient = _httpClientFactory.CreateClient();

                    using (var client = new HttpClient())
                    {

                        var request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            RequestUri = new Uri(uri)
                        };
                        var Url_1 = OktaDomain + "/api/v1/users";
                        Url_1 += strFinalSearchString;
                      
                        string _ContentType = "application/json";
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_ContentType));
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SSWS", API_Key);
                       // httpClient.DefaultRequestHeaders.Add("link", "<https://jo.oktapreview.com/api/v1/users?after=000u84fdwawXAiHhfj0h7&limit=200>\";\" rel=\"next\"");
                       // httpClient.DefaultRequestHeaders.Add("link", "<https://jo.oktapreview.com/api/v1/users?after=000u84fdwawXAiHhfj0h7&limit=200>\";\" rel=\"self\"");


                        var response = await httpClient.GetAsync(Url_1);

                        result = await response.Content.ReadAsStringAsync();

                        //string urlNext = "";
                        //////////////////////////////////////////
                        HttpHeaders headers = response.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("Link", out values))
                        {
                            if (values.Count() > 1)
                            {
                                urlNext = ((string[])values)[1];
                            }
                        }
                        MyUsers usersData = new MyUsers();
                        usersData.SearchUser = new List<ListUsers>();
                        usersData.SearchUser = JsonConvert.DeserializeObject<List<ListUsers>>(result);
                        usersData.PageLink = (!String.IsNullOrEmpty(urlNext)) ? (urlNext.Substring(1, urlNext.Length - (urlNext.Length - urlNext.IndexOf(';') + 2))) : "";
                        var res = usersData.SearchUser;

                        /* Load More 2nd Page */
                        var Url_2 = usersData.PageLink;

                        HttpResponseMessage _httpResponseMessage = await httpClient.GetAsync(Url_2);
                        var _content = _httpResponseMessage.Content;
                        _users = await _content.ReadAsStringAsync();

                        HttpHeaders _headers = _httpResponseMessage.Headers;
                        // IEnumerable<string> values;
                        if (_headers.TryGetValues("Link", out values))
                        {
                            if (values.Count() > 1)
                            {
                                _urlNext = ((string[])values)[1];
                            }
                        }

                        MyUsers _usersData = new MyUsers();
                        _usersData.SearchUser = new List<ListUsers>();
                        _usersData.SearchUser = JsonConvert.DeserializeObject<List<ListUsers>>(_users);
                        _usersData.PageLink = _urlNext.Substring(1, _urlNext.Length - (_urlNext.Length - _urlNext.IndexOf(';') + 2));

                        httpClient.DefaultRequestHeaders.Add("LoadMore", _urlNext);
                        string test = JsonConvert.SerializeObject(_usersData);
                    }
                }
                else
                {
                    /// Empty Search without any search parameters
                }
            }
            catch (AmbiguousMatchException)
            {
                throw;
            }
            return _users;
        }
    }
}
