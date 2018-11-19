using SocialMessenger.Interfaces;
using System;

namespace SocialMessenger.Utilities
{
    public class TimeSpanDisplayFormatter : ITimeSpanDisplayFormatter
    {
        public string GetFormattedDisplayString(TimeSpan timespan)
        {
            var days = timespan.Days > 0 ? $"{timespan.Days} days " : "";
            var hours = timespan.Hours > 0 ? $"{timespan.Hours} hours " : "";
            var minutes = timespan.Minutes > 0 ? $"{timespan.Minutes} minutes " : "";
            var seconds = timespan.Seconds > 0 ? $"{timespan.Seconds} seconds" : "";

            return $"{days}{hours}{minutes}{seconds}";
        }
    }
}