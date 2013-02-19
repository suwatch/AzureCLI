using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureCLI;

namespace Samples
{
    partial class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // args[0] is publishing settings file.
                string profile = args[0];

                // this will deserialize and save as global variable 
                PublishSettings.LoadFrom(profile);

                BasicTests();
                //KuduTests();
                //GitTests();
                //TfsTests();
                //GitHubTests();
                //BitbucketTests();
                //CodePlexTests();
                //DropboxTests();
                //ManagementTests();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}