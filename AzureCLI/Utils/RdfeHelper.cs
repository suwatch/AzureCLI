using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AzureCLI.Utils
{
    static class RdfeHelper
    {
        public const string MS_VERSION_HEADER = "x-ms-version";
        public const string MS_VERSION_HEADER_CONTENT = "2012-08-01";

        public static async Task PutAsync<T>(string url, T resource)
        {
            using (HttpClient client = RdfeHelper.NewHttpClient())
            {
                using (HttpResponseMessage response = await client.PutAsJsonAsync<T>(url, resource))
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        public static async Task PostAsync<T>(string url, T resource)
        {
            using (HttpClient client = RdfeHelper.NewHttpClient())
            {
                using (HttpResponseMessage response = await client.PostAsJsonAsync<T>(url, resource))
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        public static async Task DeleteAsync(string url)
        {
            using (HttpClient client = RdfeHelper.NewHttpClient())
            {
                using (HttpResponseMessage response = await client.DeleteAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        public static async Task<T> GetAsAsync<T>(string url)
        {
            using (HttpClient client = RdfeHelper.NewHttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();

                    // TODO: workaround RDFE bug that the response's header is fixed 'application/xml'
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return await response.Content.ReadAsAsync<T>();
                }
            }
        }

        public static async Task<string> GetAsStringAsync(string url)
        {
            using (HttpClient client = RdfeHelper.NewHttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }

        private static HttpClient NewHttpClient()
        {
            var handler = new WebRequestHandler();
            handler.ClientCertificates.Add(PublishProfile.Current.GetCertificate());

            var client = new HttpClient(handler);
            client.MaxResponseContentBufferSize = 2097152; // 2MB
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add(MS_VERSION_HEADER, MS_VERSION_HEADER_CONTENT);
            return client;
        }
    }
}
