using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AzureCLI;
using AzureCLI.Utils;

namespace Samples
{
    partial class Program
    {
        static void ManagementTests()
        {
            DumpCertificates();

            DumpCertificateByThumbprint("0799FBB37D9F13B1F741B96AF6FF5D0ECA0C1954");

            DumpCertificateByName("-2013-");

            CreatAndAddNewCertificate();

            DeleteCertificate("2DECAF2CF559B51A4D2B439E04D28010AFA67A36");
        }

        static void DumpCertificates()
        {
            foreach (var item in Management.GetCertificatesAsync().Result)
            {
                Console.WriteLine(item.Name + ", " + item.SubscriptionCertificateThumbprint);
            }
        }

        static void DumpCertificateByThumbprint(string thumbprint)
        {
            var item = Management.GetCertificateByThumbprintAsync(thumbprint).Result;
            Console.WriteLine(item.Name + ", " + item.SubscriptionCertificateThumbprint);
        }

        static void DumpCertificateByName(string pattern)
        {
            foreach (var item in Management.GetCertificatesByNameAsync(pattern).Result)
            {
                Console.WriteLine(item.Name + ", " + item.SubscriptionCertificateThumbprint);
            }
        }

        static void CreatAndAddNewCertificate()
        {
            var certificate = CertHelper.CreateCertificate(new X500DistinguishedName("CN=Windows Azure Tools"), "This is testing!");
            Management.AddCertificate(certificate).Wait();

            // Testing
            PublishProfile.Current.ManagementCertificate = Convert.ToBase64String(certificate.Export(X509ContentType.Pfx));
            DumpCertificates();
        }

        static void DeleteCertificate(string thumbprint)
        {
            Management.DeleteCertificate(thumbprint).Wait();
        }
    }
}
