using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureCLI;
using AzureCLI.Dropbox;

namespace Samples
{
    partial class Program
    {
        static void DropboxTests()
        {
            WebSite site;

            site = WebSite.GetAsync("azurecli01", "eastuswebspace").Result;
            site.LinkWithDropboxAsync(
                scmUri: "http://www.dropbox.com/suwatch/azure",
                token: "2rx3xoxi0atzhi1",
                tokenSecret: "h1he3nx31vezwu7",
                path: "/asp01"
            ).Wait();

            site = WebSite.GetAsync("azurecli01", "eastuswebspace").Result;
            Console.WriteLine("IsLinkedWithDropbox: " + site.IsLinkedWithDropbox());

            site = WebSite.GetAsync("azurecli01", "eastuswebspace").Result;
            site.UnlinkDropboxAsync().Wait();
        }
    }
}
