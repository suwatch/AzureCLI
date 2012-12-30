using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureCLI;
using AzureCLI.Tfs;

namespace Samples
{
    partial class Program
    {
        static void TfsTests()
        {
            WebSite site;
            
            site = WebSite.GetAsync("azurecli01", "eastuswebspace").Result;
            site.LinkWithTfsAsync(
                token: "AA1t/rcvG+DjK+eLKiy6Um5ahNMK399V3NknaDHRCMxUU=",
                refreshToken: "RAq5SzADz20rK9qtq0WmCvGWsZZdgc/slaZV+6UIFwTNQ=",
                expiresAt: "Fri, 28 Dec 2012 21:09:51 GMT",
                account: "suwatch01",
                project: "website01",
                projectGuid: "17f332b6-9e30-493b-b156-745d21a814ad"
            ).Wait();

            site = WebSite.GetAsync("azurecli01", "eastuswebspace").Result;
            Console.WriteLine("IsLinkedWithTfs: " + site.IsLinkedWithTfs());

            site = WebSite.GetAsync("azurecli01", "eastuswebspace").Result;
            site.UnlinkTfsAsync().Wait();
        }
    }
}
