using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureCLI;
using AzureCLI.Kudu;

namespace Samples
{
    partial class Program
    {
        static void KuduTests()
        {
            WebSite site;
            
            site = WebSite.GetAsync("azurecli01", "eastuswebspace").Result;
            site.EnableKuduAsync().Wait();

            site = WebSite.GetAsync("azurecli01", "eastuswebspace").Result;
            Console.WriteLine("IsKuduEnabled: " + site.IsKuduEnabled());
            Console.WriteLine("KuduDeployUri: " + site.GetKuduDeployUri());
            Console.WriteLine("KuduGitUri: " + site.GetKuduGitUriAsync().Result);
            Console.WriteLine("KuduLiveScmUri: " + site.GetKuduLiveScmUri());
        }
    }
}
