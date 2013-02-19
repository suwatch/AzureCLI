using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AzureCLI;

namespace Samples
{
    partial class Program
    {
        static void ManagementTests()
        {
            DumpCertificates();

            //DumpCertificateByThumbprint("0799FBB37D9F13B1F741B96AF6FF5D0ECA0C1954");

            //DumpCertificateByName("-2013-");

            //var cert = new X509Certificate2(fileName, password, X509KeyStorageFlags.Exportable);
            //AddCertificate(cert);    

            //DeleteCertificate("23CEBF5965E54A286E1F7B46A16312FBE1489FEF");
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

        static void AddCertificate(X509Certificate2 certificate)
        {
            Management.AddCertificate(certificate).Wait();
        }

        static void DeleteCertificate(string thumbprint)
        {
            Management.DeleteCertificate(thumbprint).Wait();
        }
    }
}
