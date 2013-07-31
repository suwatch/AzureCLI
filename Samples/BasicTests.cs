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

            //DumpWebSites();

            //CreateWebSite("azurecli01", "eastuswebspace");

            //DumpWebSite("azurecli01", "eastuswebspace");

            //DeleteWebSite("azurecli01", "eastuswebspace");

            SwapWebSite("azurestage01", "northeuropewebspace", "azureprod01");
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

        static void DumpWebSite(string name, string webSpace)
        {
            var item = WebSite.GetAsync(name, webSpace).Result;
            Console.WriteLine(item.Name);
            Console.WriteLine(item.WebSpace);
            Console.WriteLine(item);
        }

        static void SwapWebSite(string name, string webSpace, string otherName)
        {
            var itemp = WebSite.GetAsync(name, webSpace).Result;
            var otherItem = WebSite.GetAsync(otherName, webSpace).Result;
            itemp.SwapWebSite(otherItem).Wait();
        }

        static void SetGetPublishingCredentials()
        {
            var item = WebSpace.GetPublishingCredentialsAsync().Result;
            Console.WriteLine(item);

            //WebSpace.SetPublishingCredentialsAsync("username", "password").Wait();
        }
    }
}
