using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBS.Web.Utils
{
    public class AppSettingsSection
    {
        public class AppSettings
        {
            public string ApplicationName { get; set; }
            public string SessionTimeout { get; set; }
            public string UrlLogOut { get; set; }
            public string UrlHome { get; set; }
        }
    }
}
