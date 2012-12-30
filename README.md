
## Overview

This library and its samples demonstrate the features Kudu team contributes into Azure Portal (AuxPortal) such as setup Git publishing and check whether Git deployment is setup for the site.

This also includes Continuous Deployment Integration with Tfs, GitHub, Bitbucket and CodePlex.   In addition, it covers the future works (S20) to consume newly-introduced ScmType property as well as Dropbox deployment.

## Setup

Simply clone and run build.cmd (VS2012 is required).  `AzureCLI.dll` contains the library implementation while `Samples.exe` demonstrates library usages.

`Samples.exe` takes the publishing profile as an argument.  One may download such by visiting http://go.microsoft.com/fwlink/?LinkId=254432. 

## Libary Details

The default namespace `using AzureCLI;` contains common classes (WebSpace, WebSite) to navigate Azure datastructures.     

    Task<WebSpace[]> WebSpace.GetAllAsync();
    Task<WebSpace> WebSpace.GetAsync(name);

    Task<WebSite[]> WebSite.GetAllAsync();
    Task<WebSite> WebSite.GetAsync(name, webSpace);

    Task WebSite.CreateAsync(name, webSpace)
    Task WebSite.DeleteAsync(name, webSpace)

By including `using AzureCLI.Kudu;`, Kudu related functionalities are lighted up for `class WebSite`.

    Task WebSite.EnableKuduAsync();
    bool WebSite.IsKuduEnabled();
    string WebSite.GetKuduDeployUri();
    string WebSite.GetKuduLiveScmUri();
    Task<string> WebSite.GetKuduGitUriAsync();

Similarly if `using AzureCLI.Git;`, LocalGit functionalities are added.

    Task WebSite.EnableGitAsync();
    bool WebSite.IsGitEnabled();
    Task WebSite.DisableGitAsync();

For Continuous Integration (`Tfs`, `GitHub`, `Bitbucket`, `CodePlex`, `Dropbox`), same pattern applies.

    using AzureCLI.Tfs;
    
    Task WebSite.LinkWithTfsAsync(...);
    bool WebSite.IsLinkedWithTfs();
    Task WebSite.UnlinkTfsAsync();

Or

    using AzureCLI.Bitbucket;
    
    Task WebSite.LinkWithBitbucketAsync(...);
    bool WebSite.IsLinkedWithBitbucket();
    Task WebSite.UnlinkBitbucketAsync();


## TODO

  * CloudApps
