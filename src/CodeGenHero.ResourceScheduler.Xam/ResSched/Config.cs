using System.Collections.Generic;

namespace ResSched
{
    //public class Config_Example
    public class Config
    {
        //These are shared by all environments

        public static List<int> Hours = new List<int>() { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        public const string ApplicationURL = "";

        public const string MSALClientId = "7fda6409-a86f-4e4f-8d59-288588dffa46";
        public const string MSALRedirectUri = "msal7fda6409-a86f-4e4f-8d59-288588dffa46://auth";
        public static string[] MSALAuthScopes = { "User.Read" };

        public const string SlackClientId = "21052997777.497846397143";
        public const string SlackClientSecret = "4b5e8aef849a3cac0a0dfec2eba07608";
        public const string SlackRedirectUri = "http://foxbuild.ressched";
        public static string[] SlackScopes = { "users:read" };

        public static string MeetupEventsRestUrl = "https://api.meetup.com/2/events?sign=true&photo-host=public&group_id=19388316&key=561062d545032187d143d662b7df2a";

#if DEV

        public const string AppCenterUWP = "e682f484-a14d-46ea-8e08-714ff2b43dcc";
        public const string AppCenterAndroid = "30c191aa-6efe-4434-8b3d-42f0a9e817b9";
        public const string AppCenteriOS = "b3df61a0-5842-4342-a345-70f7d27f1575";

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