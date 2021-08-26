using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Specialized;
using System.Web;

namespace MyPetStore.Client.Helpers
{
    public static class ExtensionMethods
    {
        public static NameValueCollection QueryString(this NavigationManager navigationManager)
        {
            // index/find?param=1&param2=455
            // param=1&param2=455
            // [param = 1,
            // param2 = 455]
            return HttpUtility.ParseQueryString(new Uri(navigationManager.Uri).Query);
        }

        public static string QueryString(this NavigationManager navigationManager, string key)
        {
            return navigationManager.QueryString()[key];
        }

        public static string QueryString(string uri, string key)
        {
            return HttpUtility.ParseQueryString(new Uri(uri).Query)[key];
        }
    }
}