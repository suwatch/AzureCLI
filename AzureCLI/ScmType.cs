using System;

namespace AzureCLI
{
    public enum ScmType
    {
        Null,
        None,
        Tfs,
        LocalGit,
        GitHub,
        CodePlexGit,
        CodePlexHg,
        BitbucketGit,
        BitbucketHg,
        Dropbox
    }
}
