using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AzureCLI.Utils
{
    static class KuduHelper
    {
        public static async Task DeleteAsync(Uri uri)
        {
            using (HttpClient client = KuduHelper.NewHttpClient(ref uri))
            {
                using (HttpResponseMessage response = await client.DeleteAsync(uri))
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        public static async Task PutAsync<T>(Uri uri, T resource)
        {
            using (HttpClient client = KuduHelper.NewHttpClient(ref uri))
            {
                using (HttpResponseMessage response = await client.PutAsJsonAsync<T>(uri.AbsoluteUri, resource))
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        private static HttpClient NewHttpClient(ref Uri uri)
        {
            UriBuilder builder = new UriBuilder(uri);
            var handler = new HttpClientHandler();
            handler.Credentials = new NetworkCredential(builder.UserName, builder.Password);
            builder.UserName = builder.Password = null;
            uri = builder.Uri;
            
            var client = new HttpClient(handler);
            client.MaxResponseContentBufferSize = 30 * 1024 * 1024; // 30MB
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
