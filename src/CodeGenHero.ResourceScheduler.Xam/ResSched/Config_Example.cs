using System.Collections.Generic;

namespace ResSched
{
    //public class Config_Example
    public class Config
    {
        //These are shared by all environments

        public const string Preference_Email = "user_email";
        public const string Preference_LastResourceUpdate = "last_resource_update";
        public const string Preference_LastResourceScheduleUpdate = "last_schedule_update";
        public const string Preference_LastUserUpdate = "last_user_update";

        public const string BaseWebApiUrl = "";

        public static List<int> Hours = new List<int>() { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        public const string ApplicationURL = "";

        public const string MSALClientId = "";
        public const string MSALRedirectUri = "msal_______://auth";
        public static string[] MSALAuthScopes = { "User.Read" };

        public const string SlackClientId = "";
        public const string SlackClientSecret = "";
        public const string SlackRedirectUri = "";
        public static string[] SlackScopes = { "users:read" };

        public static string MeetupEventsRestUrl = "";

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