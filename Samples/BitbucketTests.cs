using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureCLI;
using AzureCLI.Bitbucket;

namespace Samples
{
    partial class Program
    {
        static void BitbucketTests()
        {
            WebSite site;
            
            site = WebSite.GetAsync("azurecli01", "eastuswebspace").Result;
            site.LinkWithBitbucketAsync("http://www.bitbucket.org/suwatch/myrepo", id_rsa: "mysshkey").Wait();

            site = WebSite.GetAsync("azurecli01", "eastuswebspace").Result;
            Console.WriteLine("IsLinkedWithBitbucket: " + site.IsLinkedWithBitbucket());

            site = WebSite.GetAsync("azurecli01", "eastuswebspace").Result;
            site.UnlinkBitbucketAsync().Wait();
        }
    }
}
