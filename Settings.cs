using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ibis_CSR_Tool
{
    public class Settings
    {
        public MyConfigurationSettings MyConfiguration { get; set; }
        public class MyConfigurationSettings
        {
            public bool MyProperty { get; set; }
        }
    }
}