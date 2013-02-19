using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AzureCLI.Utils;
using Newtonsoft.Json.Linq;

namespace AzureCLI
{
    public static class Management
    {
        public static async Task<SubscriptionCertificateList> GetCertificatesAsync()
        {
            string url = UriHelper.GetManagementCertificatesUri();
            return await RdfeHelper.GetAsXmlAsync<SubscriptionCertificateList>(url);
        }

        public static async Task<SubscriptionCertificate> GetCertificateByThumbprintAsync(string thumbprint)
        {
            string url = UriHelper.GetManagementCertificateUri(thumbprint);
            return await RdfeHelper.GetAsXmlAsync<SubscriptionCertificate>(url);
        }

        public static async Task<SubscriptionCertificateList> GetCertificatesByNameAsync(string pattern)
        {
            string url = UriHelper.GetManagementCertificatesUri();
            var certificates = await RdfeHelper.GetAsXmlAsync<SubscriptionCertificateList>(url);
            var regex = new Regex(pattern);
            return new SubscriptionCertificateList(certificates.Where(cert => regex.IsMatch(cert.Name)));
        }

        public static async Task AddCertificate(X509Certificate2 certificate)
        {
            string url = UriHelper.GetManagementCertificatesUri();
            var subscriptionCertificate = new SubscriptionCertificate
            {
                SubscriptionCertificateData = Convert.ToBase64String(certificate.Export(X509ContentType.Pfx)),
                SubscriptionCertificatePublicKey = Convert.ToBase64String(certificate.GetPublicKey()),
                SubscriptionCertificateThumbprint = certificate.Thumbprint
            };

            await RdfeHelper.PostAsync(url, subscriptionCertificate);
        }

        public static async Task DeleteCertificate(string thumbprint)
        {
            string url = UriHelper.GetManagementCertificateUri(thumbprint);
            await RdfeHelper.DeleteAsync(url);
        }
    }

    [CollectionDataContract(Name = "SubscriptionCertificates", ItemName = "SubscriptionCertificate", Namespace = "http://schemas.microsoft.com/windowsazure")]
    public class SubscriptionCertificateList : List<SubscriptionCertificate>
    {
        public SubscriptionCertificateList()
        {
        }

        public SubscriptionCertificateList(IEnumerable<SubscriptionCertificate> certificates)
            : base(certificates)
        {
        }
    }

    [DataContract(Namespace = "http://schemas.microsoft.com/windowsazure")]
    public class SubscriptionCertificate
    {
        private X509Certificate2 _certificate;

        public SubscriptionCertificate()
        {
        }

        [DataMember(EmitDefaultValue = false, Order = 3)]
        public DateTime Created { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public string SubscriptionCertificateData { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 0)]
        public string SubscriptionCertificatePublicKey { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 1)]
        public string SubscriptionCertificateThumbprint { get; set; }

        public string Name
        {
            get
            {
                var cert = this.X509Certificate;
                return !string.IsNullOrWhiteSpace(cert.FriendlyName)
                    ? cert.FriendlyName
                    : cert.GetNameInfo(X509NameType.SimpleName, false);
            }
        }
        public X509Certificate2 X509Certificate
        {
            get
            {
                if (_certificate == null)
                {
                    var data = Convert.FromBase64String(this.SubscriptionCertificateData);
                    _certificate = new X509Certificate2(data);
                }

                return _certificate;
            }
        }
    }
}
