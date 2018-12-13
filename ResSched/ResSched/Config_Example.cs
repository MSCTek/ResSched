using System;
using System.Collections.Generic;
using System.Text;

namespace ResSched
{
    //public class Config_Example
    public partial class Config
    {
        //These are shared by all environments
        public const string MSALClientId = "";
        public const string MSALRedirectUri = "msal_______://auth";
        public static string[] MSALAuthScopes = { "User.Read" };

        public const string SlackClientId = "";
        public const string SlackClientSecret = "";
        public const string SlackRedirectUri = "";
        public static string[] SlackScopes = { "users:read" };

#if DEV

        public const string AppCenterUWP = "";
        public const string AppCenterAndroid = "";
        public const string AppCenteriOS = "";

#endif

#if QA

        public const string AppCenterUWP = "";
        public const string AppCenterAndroid = "";
        public const string AppCenteriOS = "";

#endif

#if PROD

        public const string AppCenterUWP = "";
        public const string AppCenterAndroid = "";
        public const string AppCenteriOS = "";

#endif

    }
}
