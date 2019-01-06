using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ResSched.Helpers
{
    public static class HTTPClientService
    {
        public static async Task<Models.MeetupEvents.RootObject> RefreshDataAsync()
        {
            var Events = new Models.MeetupEvents.RootObject();

            try
            {
                HttpClient client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                var uri = new Uri(Config.MeetupEventsRestUrl);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Events = JsonConvert.DeserializeObject<Models.MeetupEvents.RootObject>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
                Crashes.TrackError(ex);
            }

            return Events;
        }
    }
}