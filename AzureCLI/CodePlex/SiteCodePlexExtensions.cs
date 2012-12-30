using System.Linq;
using System.Threading.Tasks;
using AzureCLI.Kudu;
using AzureCLI.Utils;

namespace AzureCLI.CodePlex
{
    public static partial class WebSiteExtensions
    {
        public static bool IsLinkedWithCodePlex(this WebSite site, bool isMercurial = false)
        {
            if (site.IsKuduEnabled())
            {
                ScmType scmType = site.GetScmType();
                return scmType == ScmType.Null || scmType == (isMercurial ? ScmType.CodePlexHg : ScmType.CodePlexGit);
            }

            return false;
        }

        public static async Task LinkWithCodePlexAsync(this WebSite site, string scmUri, bool isMercurial = false)
        {
            await site.EnsureKuduEnabled();

            var metadata = site.SiteProperties.Metadata.Where(p => p.Name != Constants.ScmUri).ToList();
            metadata.Add(new NameValuePair { Name = Constants.ScmUri, Value = scmUri });

            string url = UriHelper.GetWebSiteConfigUri(site.WebSpace, site.Name);
            await RdfeHelper.PutAsync(url, new { ScmType = (isMercurial ? ScmType.CodePlexHg : ScmType.CodePlexGit).ToString(), Metadata = metadata });
        }

        public static async Task UnlinkCodePlexAsync(this WebSite site)
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
