using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ibis_CSR_Tool.Models
{
    public class LinkUser
    {
        public string Id { get; set; }
        public string IbisID { get; set; }
        public string IbisAccessRequestGroupID { get; set; }
        public string IbisGroupID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string DriversLicense { get; set; }
        public string BirthDate { get; set; }
        public string StreetAddress { get; set; }
        public string PrimaryPhone { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
