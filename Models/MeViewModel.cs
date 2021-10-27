using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ibis_CSR_Tool.Models
{
    public class MeViewModel
    {
        public string Username { get; set; }
        public bool SdkAvailable { get; set; }
        public dynamic UserInfo { get; set; }
        public IEnumerable<string> Groups { get; set; }
    }
}
