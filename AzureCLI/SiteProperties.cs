using System;

namespace AzureCLI
{
    public class SiteProperties
    {
        public NameValuePair[] AppSettings { get; set; }
        public NameValuePair[] Metadata { get; set; }
        public NameValuePair[] Properties { get; set; }
    }
}
