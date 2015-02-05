using System;
using System.Threading.Tasks;
using AzureCLI.Utils;
using Newtonsoft.Json.Linq;

namespace AzureCLI
{
    public enum WorkerSizeOptions
    {
        Small = 0,
        Medium = 1,
        Large = 2
    }

    public enum StatusOptions
    {
        Ready = 0,
        Pending = 1
    }

    public class WebHostingPlan
    {
        private WebHostingPlan()
        {
        }

        public string Name { get; set; }
        public string SKU { get; set; }
        public WorkerSizeOptions? WorkerSize { get; set; }
        public int? WorkerSizeId { get; set; }
        public int? NumberOfWorkers { get; set; }
        public WorkerSizeOptions? CurrentWorkerSize { get; set; }
        public int? CurrentWorkerSizeId { get; set; }
        public int? CurrentNumberOfWorkers { get; set; }
        public StatusOptions Status { get; set; }
        public string WebSpace { get; set; }
        public string Subscription { get; set; }
        public string AdminSiteName { get; set; }
        public string AlternateUniverse { get; set; }

        public static async Task<WebHostingPlan[]> GetAllAsync(string webSpace)
        {
            string url = UriHelper.GetWebHostingPlansUri(webSpace);
            return await RdfeHelper.GetAsAsync<WebHostingPlan[]>(url);
        }

        public static async Task<WebHostingPlan> GetAsync(string webSpace, string webHostingPlan)
        {
            string url = UriHelper.GetWebHostingPlanUri(webSpace, webHostingPlan);
            return await RdfeHelper.GetAsAsync<WebHostingPlan>(url);
        }

        public static async Task DeleteAsync(string webSpace, string webHostingPlan)
        {
            string url = UriHelper.GetWebHostingPlanUri(webSpace, webHostingPlan);
            await RdfeHelper.DeleteAsync(url);
        }

        public static async Task UpdateAdminSite(string webSpace, string webHostingPlan, string adminSite)
        {
            string url = UriHelper.GetWebHostingPlanUri(webSpace, webHostingPlan);
            // Name property is to work around the Operation lock ANT43 issue
            await RdfeHelper.PutAsync(url, new { Name = webHostingPlan, AdminSiteName = adminSite });
        }

        public override string ToString()
        {
            return JObject.FromObject(this).ToString();
        }
    }
}
