using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureCLI;
using AzureCLI.CodePlex;

namespace Samples
{
    partial class Program
    {
        static void CodePlexTests()
        {
            WebSite site;
            
            site = WebSite.GetAsync("azurecli01", "eastuswebspace").Result;
            Console.WriteLine(site);
            site.LinkWithCodePlexAsync("http://www.codeplex.com/suwatch/myrepo", isMercurial: true).Wait();
            

            site = WebSite.GetAsync("azurecli01", "eastuswebspace").Result;
            Console.WriteLine(site);
            Console.WriteLine("IsLinkedWithCodePlex: " + site.IsLinkedWithCodePlex());

            site = WebSite.GetAsync("azurecli01", "eastuswebspace").Result;
            Console.WriteLine(site);
            site.UnlinkCodePlexAsync().Wait();
        }
    }
}
