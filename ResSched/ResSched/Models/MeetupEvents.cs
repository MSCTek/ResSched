using System;
using System.Collections.Generic;

namespace ResSched.Models.MeetupEvents
{
    public class Group
    {
        public object created { get; set; }
        public double group_lat { get; set; }
        public double group_lon { get; set; }
        public int id { get; set; }
        public string join_mode { get; set; }
        public string name { get; set; }
        public string urlname { get; set; }
        public string who { get; set; }
    }

    public class Meta
    {
        public int count { get; set; }
        public string description { get; set; }
        public string id { get; set; }
        public string lat { get; set; }
        public string link { get; set; }
        public string lon { get; set; }
        public string method { get; set; }
        public string next { get; set; }
        public string signed_url { get; set; }
        public string title { get; set; }
        public int total_count { get; set; }
        public long updated { get; set; }
        public string url { get; set; }
    }

    public class Result
    {
        public object created { get; set; }
        public string description { get; set; }
        public string descriptionDisplay { get { return description.Replace("<p>", string.Empty); } }
        public int duration { get; set; }
        public string event_url { get; set; }
        public Group group { get; set; }
        public int headcount { get; set; }
        public string how_to_find_us { get; set; }
        public string id { get; set; }
        public int maybe_rsvp_count { get; set; }
        public string name { get; set; }
        public string photo_url { get; set; }
        public string status { get; set; }
        public long time { get; set; }
        public DateTime timeDisplay { get { return new DateTime(time); } }
        public object updated { get; set; }
        public int utc_offset { get; set; }
        public Venue venue { get; set; }
        public string visibility { get; set; }
        public int waitlist_count { get; set; }
        public int yes_rsvp_count { get; set; }
    }

    public class RootObject
    {
        public Meta meta { get; set; }
        public List<Result> results { get; set; }
    }

    public class Venue
    {
        public string address_1 { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public int id { get; set; }
        public double lat { get; set; }
        public string localized_country_name { get; set; }
        public double lon { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public bool repinned { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
    }
}