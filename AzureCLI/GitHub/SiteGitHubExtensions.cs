using System;
using System.Linq;
using System.Threading.Tasks;
using AzureCLI.Kudu;
using AzureCLI.Utils;

namespace AzureCLI.GitHub
{
    public static partial class WebSiteExtensions
    {
        public static bool IsLinkedWithGitHub(this WebSite site)
        {
            if (site.IsKuduEnabled())
            {
                ScmType scmType = site.GetScmType();
                return scmType == ScmType.Null || scmType == ScmType.GitHub;
            }

            return false;
        }

        public static async Task LinkWithGitHubAsync(this WebSite site, string scmUri, string id_rsa = null)
        {
            await site.EnsureKuduEnabled();

            var metadata = site.SiteProperties.Metadata.Where(p => p.Name != Constants.ScmUri).ToList();
            metadata.Add(new NameValuePair { Name = Constants.ScmUri, Value = scmUri });

            string url = UriHelper.GetWebSiteConfigUri(site.WebSpace, site.Name);
            await Task.WhenAll(
                RdfeHelper.PutAsync(url, new { ScmType = ScmType.GitHub.ToString(), Metadata = metadata }),
                !String.IsNullOrEmpty(id_rsa) ? site.SetKuduSSHKeyAsync(id_rsa) : Task.FromResult(true)
            );
        }

        public static async Task UnlinkGitHubAsync(this WebSite site)
        {
            if (site.IsKuduEnabled())
            {
                var metadata = site.SiteProperties.Metadata.Where(p =>
                    p.Name != Constants.ScmUri
                ).ToArray();

                string url = UriHelper.GetWebSiteConfigUri(site.WebSpace, site.Name);
                await RdfeHelper.PutAsync(url, new { ScmType = ScmType.LocalGit.ToString(), Metadata = metadata });
            }
        }
    }
}
