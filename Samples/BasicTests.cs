using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureCLI;

namespace Samples
{
    partial class Program
    {
        static void BasicTests()
        {
            //SetGetPublishingCredentials();

            //DumpWebSpaces();

            //DumpWebSpace("eastuswebspace");

            DumpWebHostingPlans("westuswebspace");

            //DumpWebSites();

            //CreateWebSite("azurecli01", "eastuswebspace");

            //DumpWebSite("azurecli01", "eastuswebspace");

            //DeleteWebSite("azurecli01", "eastuswebspace");

            //SwapWebSiteSlots("suwatchhk02", "eastasiawebspace");

            //SyncWebSiteRepository("suwatchdb02", "eastasiawebspace");
        }

        static void DumpWebSpaces()
        {
            foreach (var item in WebSpace.GetAllAsync().Result)
            {
                Console.WriteLine(item.Name);
                Console.WriteLine(item);
            }
        }

        static void DumpWebSpace(string name)
        {
            var item = WebSpace.GetAsync(name).Result;
            Console.WriteLine(item.Name);
            Console.WriteLine(item);
        }

        static void DumpWebHostingPlans(string name)
        {
            foreach (var item in WebHostingPlan.GetAllAsync(name).Result)
            {
                Console.WriteLine(item.Name);
                Console.WriteLine(item);
            }

            //Console.WriteLine(WebHostingPlan.GetAsync(name, "Default1").Result);
            //WebHostingPlan.UpdateAdminSite(name, "Default1", "suwatadmin").Wait();
        }

        static void DumpWebSites()
        {
            foreach (var item in WebSite.GetAllAsync().Result)
            {
                Console.WriteLine(item.Name);
                Console.WriteLine(item.WebSpace);
                Console.WriteLine(item);
            }
        }

        static void CreateWebSite(string name, string webSpace)
        {
            WebSite.CreateAsync(name, webSpace).Wait();
        }

        static void DeleteWebSite(string name, string webSpace)
        {
            WebSite.DeleteAsync(name, webSpace).Wait();
        }

        static void SyncWebSiteRepository(string name, string webSpace)
        {
            WebSite.SyncWebSiteRepositoryAsync(name, webSpace).Wait();
        }

        static void DumpWebSite(string name, string webSpace)
        {
            var item = WebSite.GetAsync(name, webSpace).Result;
            Console.WriteLine(item.Name);
            Console.WriteLine(item.WebSpace);
            Console.WriteLine(item);
        }

        static void SwapWebSiteSlots(string name, string webSpace)
        {
            var itemp = WebSite.GetAsync(name, webSpace).Result;
            itemp.SwapWebSiteSlots().Wait();
        }

        static void SetGetPublishingCredentials()
        {
            var item = WebSpace.GetPublishingCredentialsAsync().Result;
            Console.WriteLine(item);

            //WebSpace.SetPublishingCredentialsAsync("username", "password").Wait();
        }
    }
}
