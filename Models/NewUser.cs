using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ibis_CSR_Tool.Models
{
    public class NewUser
    {
        public string Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string login { get; set; }
        public string email { get; set; }
        public string ibisid { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string streetAddress { get; set; }
        public string zipCode { get; set; }
        public string drivers_license { get; set; }
        public string birthdate { get; set; }
        public string answer { get; set; }
        public string password { get; set; }
        public string question { get; set; }
        public string primaryPhone { get; set; }
    }
}
