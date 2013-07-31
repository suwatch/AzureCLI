using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AzureCLI.Utils;
using Newtonsoft.Json.Linq;

namespace AzureCLI
{
    public class WebSite
    {
        static WebSite()
        {
            HostSuffix = HostSuffix ?? ".azurewebsites.net";
        }

        private WebSite()
        {
        }

        public static string HostSuffix { get; set; }

        public string Name { get; set; }
        public string WebSpace { get; set; }
        public string State { get; set; }
        public bool Enabled { get; set; }
        public string[] HostNames { get; set; }
        public string SelfLink { get; set; }
        public string SiteMode { get; set; }
        public SiteProperties SiteProperties { get; set; }

        public static async Task<WebSite[]> GetAllAsync()
        {
            var tasks = new List<Task<WebSite[]>>();
            foreach (WebSpace webSpace in await AzureCLI.WebSpace.GetAllAsync())
            {
                tasks.Add(WebSite.GetAllAsync(webSpace.Name));
            }

            var results = await Task.WhenAll<WebSite[]>(tasks.ToArray());
            var sites = new List<WebSite>();
            foreach (var result in results)
            {
                sites.AddRange(result);
            }

            return sites.ToArray(); 
        }

        public static async Task<WebSite[]> GetAllAsync(string webSpace)
        {
            string url = UriHelper.GetWebSitesUri(webSpace);
            return await RdfeHelper.GetAsAsync<WebSite[]>(url);
        }

        public static async Task<WebSite> GetAsync(string name, string webSpace)
        {
            string url = UriHelper.GetWebSiteUri(webSpace, name);
            return await RdfeHelper.GetAsAsync<WebSite>(url);
        }

        public static async Task CreateAsync(string name, string webSpace)
        {
            string url = UriHelper.GetWebSitesUri(webSpace, includesProperties: false);
            await RdfeHelper.PostAsync(url, new
            {
                Name = name,
                HostNames = new string[] { name + WebSite.HostSuffix }
            });
        }

        public static async Task DeleteAsync(string name, string webSpace)
        {
            string url = UriHelper.GetWebSiteUri(webSpace, name, includesProperties: false);
            await RdfeHelper.DeleteAsync(url);
        }

        public async Task SwapWebSite(WebSite otherSite)
        {
            string url = UriHelper.GetSwapWebSiteUrl(WebSpace, Name, otherSite.Name);
            await RdfeHelper.PostAsync(url, String.Empty);
        }

        public override string ToString()
        {
            return JObject.FromObject(this).ToString();
        }
    }
}
