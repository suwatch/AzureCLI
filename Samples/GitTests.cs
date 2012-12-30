using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureCLI;
using AzureCLI.Git;

namespace Samples
{
    partial class Program
    {
        static void GitTests()
        {
            WebSite site;
            
            site = WebSite.GetAsync("azurecli01", "eastuswebspace").Result;
            site.EnableGitAsync().Wait();

            site = WebSite.GetAsync("azurecli01", "eastuswebspace").Result;
            Console.WriteLine("IsGitEnabled: " + site.IsGitEnabled());

            site = WebSite.GetAsync("azurecli01", "eastuswebspace").Result;
            site.DisableGitAsync().Wait();
        }
    }
}
