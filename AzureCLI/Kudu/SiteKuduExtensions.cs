using System;
using System.Linq;
using System.Threading.Tasks;
using AzureCLI.Utils;

namespace AzureCLI.Kudu
{
    public static partial class WebSiteExtensions
    {
        public static bool IsKuduEnabled(this WebSite site)
        {
            string repositoryUri = (from p in site.SiteProperties.Properties
                                    where p.Name == Constants.RepositoryUri
                                    select p.Value).First();
            return !String.IsNullOrEmpty(repositoryUri);
        }

        public static async Task EnableKuduAsync(this WebSite site)
        {
            string url = UriHelper.GetWebSiteRepositoryUri(site.WebSpace, site.Name);
            await RdfeHelper.PostAsync(url, new { Name = site.Name, HostNames = site.HostNames });
        }

        public static Uri GetKuduDeployUri(this WebSite site)
        {
            return GetKuduServiceUri(site, "deploy");
        }

        public static async Task<Uri> GetKuduGitUriAsync(this WebSite site)
        {
            PublishingCredentials credentials = await WebSpace.GetPublishingCredentialsAsync();

            string repositoryUri = (from p in site.SiteProperties.Properties
                                    where p.Name == Constants.RepositoryUri
                                    select p.Value).First();

            UriBuilder uri = new UriBuilder(repositoryUri);
            uri.UserName = credentials.PublishingUserName;
            uri.Path = site.Name + ".git";
            return uri.Uri;
        }

        public static Uri GetKuduLiveScmUri(this WebSite site)
        {
            return GetKuduServiceUri(site, "live/scm");
        }

        public static Uri GetKuduServiceUri(WebSite site, string path)
        {
            string repositoryUri = (from p in site.SiteProperties.Properties
                                    where p.Name == Constants.RepositoryUri
                                    select p.Value).First();

            string userName = (from p in site.SiteProperties.Properties
                               where p.Name == Constants.PublishingUsername
                               select p.Value).First();

            string password = (from p in site.SiteProperties.Properties
                               where p.Name == Constants.PublishingPassword
                               select p.Value).First();

            UriBuilder uri = new UriBuilder(repositoryUri);
            uri.UserName = userName;
            uri.Password = password;
            uri.Path = path;
            return uri.Uri;
        }

        internal static async Task EnsureKuduEnabled(this WebSite site)
        {
            if (!site.IsKuduEnabled())
            {
                await site.EnableKuduAsync();
            }
        }

        internal static async Task SetKuduSSHKeyAsync(this WebSite site, string id_rsa)
        {
            Uri uri = GetKuduServiceUri(site, "sshkey");
            await KuduHelper.PutAsync(uri, new { key = id_rsa });
        }

        internal static ScmType GetScmType(this WebSite site)
        {
            string scmType = (from p in site.SiteProperties.Properties
                                    where p.Name == Constants.ScmType
                                    select p.Value).FirstOrDefault();

            if (String.IsNullOrEmpty(scmType))
            {
                return ScmType.Null;
            }

            return (ScmType)Enum.Parse(typeof(ScmType), scmType);
        }
    }
}
