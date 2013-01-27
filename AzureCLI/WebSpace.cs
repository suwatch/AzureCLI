using System;
using System.Threading.Tasks;
using AzureCLI.Utils;
using Newtonsoft.Json.Linq;

namespace AzureCLI
{
    public class WebSpace
    {
        private WebSpace()
        {
        }

        public string Name { get; set; }
        public string GeoLocation { get; set; }
        public string GeoRegion { get; set; }
        public int Status { get; set; }
        public int AvailabilityState { get; set; }
        public Nullable<int> ComputeMode { get; set; }
        public Nullable<int> CurrentNumberOfWorkers { get; set; }
        public Nullable<int> CurrentWorkerSize { get; set; }
        public int NumberOfWorkers { get; set; }
        public string Plan { get; set; }
        public string Subscription { get; set; }
        public Nullable<int> WorkerSize { get; set; }

        public static async Task<WebSpace[]> GetAllAsync()
        {
            string url = UriHelper.GetWebSpacesUri();
            return await RdfeHelper.GetAsAsync<WebSpace[]>(url);
        }

        public static async Task<WebSpace> GetAsync(string name)
        {
            string url = UriHelper.GetWebSpaceUri(name);
            return await RdfeHelper.GetAsAsync<WebSpace>(url);
        }

        public static async Task SetPublishingCredentialsAsync(string userName, string password)
        {
            string url = UriHelper.GetPublishingCredentialsUri();
            await RdfeHelper.PutAsync(url, new PublishingCredentials { PublishingUserName = userName, PublishingPassword = password });
        }

        public static async Task<PublishingCredentials> GetPublishingCredentialsAsync()
        {
            string url = UriHelper.GetPublishingCredentialsUri();
            return await RdfeHelper.GetAsAsync<PublishingCredentials>(url);
        }

        public override string ToString()
        {
            return JObject.FromObject(this).ToString();
        }
    }
}
