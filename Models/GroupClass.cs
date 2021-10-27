using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ibis_CSR_Tool.Models
{
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Profile
        {
            public string name { get; set; }
            public string description { get; set; }
        }

        public class Logo
        {
            public string name { get; set; }
            public string href { get; set; }
            public string type { get; set; }
        }

        public class Users
        {
            public string href { get; set; }
        }

        public class Apps
        {
            public string href { get; set; }
        }

        public class Links
        {
            public List<Logo> logo { get; set; }
            public Users users { get; set; }
            public Apps apps { get; set; }
        }

        public class GroupClass
        {
            public string id { get; set; }
            public DateTime created { get; set; }
            public DateTime lastUpdated { get; set; }
            public DateTime lastMembershipUpdated { get; set; }
            public List<string> objectClass { get; set; }
            public string type { get; set; }
            public Profile profile { get; set; }
            public Links _links { get; set; }
        }

    }
