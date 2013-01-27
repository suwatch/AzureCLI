using System;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;

namespace AzureCLI
{
    public class PublishingCredentials
    {
        public string Name { get; set; }

        public string PublishingPassword { get; set; }

        public string PublishingUserName { get; set; }

        public override string ToString()
        {
            return JObject.FromObject(this).ToString();
        }
    }
}
