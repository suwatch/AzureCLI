using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureCLI.Utils
{
    static class UriHelper
    {
        private const string PropertiesToInclude = "propertiesToInclude=repositoryuri,metadata,scmtype,appsettings,publishingusername,publishingpassword";

        public static string GetPublishingUsersUri()
        {
            PublishProfile profile = PublishProfile.Current;
            return String.Format("{0}/{1}/services/webspaces/?properties=publishingusers",
                profile.Url.TrimEnd('/'),
                profile.GetSubscriptionId());
        }

        public static string GetWebSpacesUri()
        {
            PublishProfile profile = PublishProfile.Current;
            return String.Format("{0}/{1}/services/webspaces/",
                profile.Url.TrimEnd('/'),
                profile.GetSubscriptionId());
        }

        public static string GetWebSpaceUri(string webSpace)
        {
            PublishProfile profile = PublishProfile.Current;
            return String.Format("{0}/{1}/services/webspaces/{2}/",
                profile.Url.TrimEnd('/'),
                profile.GetSubscriptionId(),
                webSpace);
        }

        public static string GetWebSitesUri(string webSpace, bool includesProperties = true)
        {
            PublishProfile profile = PublishProfile.Current;
            return String.Format("{0}/{1}/services/webspaces/{2}/sites{3}",
                profile.Url.TrimEnd('/'),
                profile.GetSubscriptionId(),
                webSpace,
                includesProperties ? ("/?" + PropertiesToInclude) : String.Empty);
        }

        public static string GetWebSiteUri(string webSpace, string siteName, bool includesProperties = true)
        {
            PublishProfile profile = PublishProfile.Current;
            return String.Format("{0}/{1}/services/webspaces/{2}/sites/{3}{4}",
                profile.Url.TrimEnd('/'),
                profile.GetSubscriptionId(),
                webSpace,
                siteName,
                includesProperties ? ("/?" + PropertiesToInclude) : String.Empty);
        }

        public static string GetWebSiteConfigUri(string webSpace, string siteName)
        {
            PublishProfile profile = PublishProfile.Current;
            return String.Format("{0}/{1}/services/webspaces/{2}/sites/{3}/config",
                profile.Url.TrimEnd('/'),
                profile.GetSubscriptionId(),
                webSpace,
                siteName);
        }

        public static string GetWebSiteRepositoryUri(string webSpace, string siteName)
        {
            PublishProfile profile = PublishProfile.Current;
            return String.Format("{0}/{1}/services/webspaces/{2}/sites/{3}/repository",
                profile.Url.TrimEnd('/'),
                profile.GetSubscriptionId(),
                webSpace,
                siteName);
        }
    }
}
