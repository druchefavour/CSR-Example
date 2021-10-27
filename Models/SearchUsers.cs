using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ibis_CSR_Tool
{
       // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Type
    {
        public string id { get; set; }
    }

    public class Profile
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string primaryPhone { get; set; }
        public string zipCode { get; set; }
        public string mobilePhone { get; set; }
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string countryCode { get; set; }
        public object secondEmail { get; set; }
        public string state { get; set; }
        public string login { get; set; }
        public string email { get; set; }
    }

    public class Email
    {
        public string value { get; set; }
        public string status { get; set; }
        public string type { get; set; }
    }

    public class Provider
    {
        public string type { get; set; }
        public string name { get; set; }
    }

    public class Credentials
    {
        public List<Email> emails { get; set; }
        public Provider provider { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
    }

    public class ListUsers
    {
        public string id { get; set; }
        public string status { get; set; }
        public DateTime created { get; set; }
        public object activated { get; set; }
        public object statusChanged { get; set; }
        public object lastLogin { get; set; }
        public DateTime lastUpdated { get; set; }
        public object passwordChanged { get; set; }
        public Type type { get; set; }
        public Profile profile { get; set; }
        public Credentials credentials { get; set; }
        public Links _links { get; set; }
    }

    public class MyUsers
    {
        //[JsonPropertyName("searchUsers")]
        public List<ListUsers> SearchUser { get; set; }

        //[JsonPropertyName("pagelink")]
        public string PageLink { get; set; }
    }
  
    public class SearchParameters //Class A
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string login { get; set; }
        public string email { get; set; }
        public string ibisid { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        //private SearchParameters() { } // mark this private so that no other instances of A can be created

       // public static readonly SearchParameters SearchParametersInstance = new SearchParameters();
    }
}