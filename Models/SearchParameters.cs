using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ibis_CSR_Tool.Models
{
    public class SearchParameters
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string login { get; set; }
        public string email { get; set; }
        public string ibisid { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public string drivers_license { get; set; }
        //public SearchParameters() { } // mark this private so that no other instances of A can be created

        //public static readonly SearchParameters SearchParametersInstance = new SearchParameters();
    }
}
