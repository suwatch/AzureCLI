using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using AzureCLI.Utils;

namespace AzureCLI
{
    public class PublishProfile
    {
        public PublishProfile()
        {
            Current = this;
        }

        public static PublishProfile Current { get; private set; }

        [XmlAttribute] 
        public string PublishMethod { get; set; }

        [XmlAttribute] 
        public string Url { get; set; }

        [XmlAttribute]
        public string ManagementCertificate { get; set; }
        
        [XmlAttribute]
        public string ManagementCertificatePassword { get; set; }

        [XmlElement(ElementName = "Subscription")]
        public Subscription[] Subscriptions { get; set; }

        internal string GetSubscriptionId()
        {
            return this.Subscriptions[0].Id;
        }

        internal X509Certificate2 GetCertificate()
        {
            return new X509Certificate2(Convert.FromBase64String(this.ManagementCertificate), this.ManagementCertificatePassword);
        }

        internal static async Task<string[]> GetPublishingUsersAsync()
        {
            string url = UriHelper.GetPublishingUsersUri();
            return await RdfeHelper.GetAsAsync<string[]>(url);
        }
        
        public class Subscription
        {
            [XmlAttribute]
            public string Id { get; set; }

            [XmlAttribute]
            public string Name { get; set; }
        }
    }
}
