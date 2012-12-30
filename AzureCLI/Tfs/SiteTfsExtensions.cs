using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureCLI.Kudu;
using AzureCLI.Utils;

namespace AzureCLI.Tfs
{
    public static partial class WebSiteExtensions
    {
        public const string TfsOAuthTokenKey = "tfsauthtoken";
        public const string TfsOAuthTokenExpireTimeKey = "tfsauthtoken_expiresat";
        public const string TfsLinkedAccntKey = "tfslinkacct";
        public const string TfsLinkedProjectKey = "tfslinkproj";
        public const string TfsLinkedProjectGuidKey = "tfslinkprojguid";
        public const string TfsRefreshTokenKey = "tfsrefreshtoken";

        public static bool IsLinkedWithTfs(this WebSite site)
        {
            ScmType scmType = site.GetScmType();
            if (scmType == ScmType.Null)
            {
                // Legacy
                return site.SiteProperties.Metadata.Any(p => p.Name == TfsLinkedAccntKey)
                    && site.SiteProperties.Metadata.Any(p => p.Name == TfsOAuthTokenKey)
                    && site.SiteProperties.Metadata.Any(p => p.Name == TfsOAuthTokenExpireTimeKey);
            }

            return scmType == ScmType.Tfs;
        }

        public static async Task LinkWithTfsAsync(this WebSite site,
            string token,
            string refreshToken,
            string expiresAt,
            string account,
            string project,
            string projectGuid)
        {
            List<NameValuePair> metadata = site.SiteProperties.Metadata.Where(p =>
                p.Name != TfsOAuthTokenKey
                && p.Name != TfsOAuthTokenExpireTimeKey
                && p.Name != TfsLinkedAccntKey
                && p.Name != TfsLinkedProjectKey
                && p.Name != TfsLinkedProjectGuidKey
                && p.Name != TfsRefreshTokenKey
            ).ToList();

            metadata.Add(new NameValuePair { Name = TfsOAuthTokenKey, Value = token });
            metadata.Add(new NameValuePair { Name = TfsOAuthTokenExpireTimeKey, Value = expiresAt });
            metadata.Add(new NameValuePair { Name = TfsLinkedAccntKey, Value = account });
            metadata.Add(new NameValuePair { Name = TfsLinkedProjectKey, Value = project });
            metadata.Add(new NameValuePair { Name = TfsLinkedProjectGuidKey, Value = projectGuid });
            metadata.Add(new NameValuePair { Name = TfsRefreshTokenKey, Value = refreshToken });

            string url = UriHelper.GetWebSiteConfigUri(site.WebSpace, site.Name);
            await RdfeHelper.PutAsync(url, new { ScmType = ScmType.Tfs.ToString(), Metadata = metadata.ToArray() });
        }

        public static async Task UnlinkTfsAsync(this WebSite site)
        {
            NameValuePair[] metadata = site.SiteProperties.Metadata.Where(p =>
                p.Name != TfsOAuthTokenKey
                && p.Name != TfsOAuthTokenExpireTimeKey
                && p.Name != TfsLinkedAccntKey
                && p.Name != TfsLinkedProjectKey
                && p.Name != TfsLinkedProjectGuidKey
                && p.Name != TfsRefreshTokenKey
            ).ToArray();

            string url = UriHelper.GetWebSiteConfigUri(site.WebSpace, site.Name);
            await RdfeHelper.PutAsync(url, new { ScmType = ScmType.None.ToString(), Metadata = metadata });
        }
    }
}
