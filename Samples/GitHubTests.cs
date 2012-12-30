using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureCLI;
using AzureCLI.GitHub;

namespace Samples
{
    partial class Program
    {
        static void GitHubTests()
        {
            WebSite site;
            
            site = WebSite.GetAsync("azurecli01", "eastuswebspace").Result;
            site.LinkWithGitHubAsync("http://www.github.com/suwatch/myrepo").Wait();

            site = WebSite.GetAsync("azurecli01", "eastuswebspace").Result;
            Console.WriteLine("IsLinkedWithGitHub: " + site.IsLinkedWithGitHub());

            site = WebSite.GetAsync("azurecli01", "eastuswebspace").Result;
            site.UnlinkGitHubAsync().Wait();
        }
    }
}
