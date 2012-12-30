using System.Linq;
using System.Threading.Tasks;
using AzureCLI.Kudu;
using AzureCLI.Utils;

namespace AzureCLI.Dropbox
{
    public static partial class WebSiteExtensions
    {
        public const string DropboxTokenKey = "dropbox_token";
        public const string DropboxTokenSecretKey = "dropbox_token_secret";
        public const string DropboxPathKey = "dropbox_path";

        public static bool IsLinkedWithDropbox(this WebSite site)
        {
            if (site.IsKuduEnabled())
            {
                ScmType scmType = site.GetScmType();
                return scmType == ScmType.Null || scmType == ScmType.Dropbox;
            }

            return false;
        }

        public static async Task LinkWithDropboxAsync(this WebSite site, string scmUri, string token, string tokenSecret, string path)
        {
            await site.EnsureKuduEnabled();

            var metadata = site.SiteProperties.Metadata.Where(p =>
                p.Name != Constants.ScmUri
                && p.Name != DropboxTokenKey
                && p.Name != DropboxTokenSecretKey
                && p.Name != DropboxPathKey
            ).ToList();

            metadata.Add(new NameValuePair { Name = Constants.ScmUri, Value = scmUri });
            metadata.Add(new NameValuePair { Name = DropboxTokenKey, Value = token });
            metadata.Add(new NameValuePair { Name = DropboxTokenSecretKey, Value = tokenSecret });
            metadata.Add(new NameValuePair { Name = DropboxPathKey, Value = path });

            string url = UriHelper.GetWebSiteConfigUri(site.WebSpace, site.Name);
            await RdfeHelper.PutAsync(url, new { ScmType = ScmType.Dropbox.ToString(), Metadata = metadata.ToArray() });
        }

        public static async Task UnlinkDropboxAsync(this WebSite site)
        {
            if (site.IsKuduEnabled())
            {
                var metadata = site.SiteProperties.Metadata.Where(p =>
                    p.Name != Constants.ScmUri
                    && p.Name != DropboxTokenKey
                    && p.Name != DropboxTokenSecretKey
                    && p.Name != DropboxPathKey
                ).ToArray();

                string url = UriHelper.GetWebSiteConfigUri(site.WebSpace, site.Name);
                await RdfeHelper.PutAsync(url, new { ScmType = ScmType.LocalGit.ToString(), Metadata = metadata });
            }
        }
    }
}
