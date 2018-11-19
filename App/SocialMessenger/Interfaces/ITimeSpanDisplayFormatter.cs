using System;

namespace SocialMessenger.Interfaces
{
    public interface ITimeSpanDisplayFormatter
    {
        string GetFormattedDisplayString(TimeSpan timespan);
    }
}