using System;
using System.Linq;
using System.Threading.Tasks;
using AzureCLI.Kudu;
using AzureCLI.Utils;

namespace AzureCLI.Git
{
    public static partial class WebSiteExtensions
    {
        public static bool IsGitEnabled(this WebSite site)
        {
            if (site.IsKuduEnabled())
            {
                ScmType scmType = site.GetScmType();
                return scmType == ScmType.Null || scmType == ScmType.LocalGit;
            }

            return false;
        }

        public static async Task EnableGitAsync(this WebSite site)
        {
            await site.EnsureKuduEnabled();

            string url = UriHelper.GetWebSiteConfigUri(site.WebSpace, site.Name);
            await RdfeHelper.PutAsync(url, new { ScmType = ScmType.LocalGit.ToString() });
        }

        public static async Task DisableGitAsync(this WebSite site)
        {
            if (site.IsKuduEnabled())
            {
                string url = UriHelper.GetWebSiteConfigUri(site.WebSpace, site.Name);
                await RdfeHelper.PutAsync(url, new { ScmType = ScmType.None.ToString() });

                await KuduHelper.DeleteAsync(site.GetKuduLiveScmUri());
            }
        }
    }
}
