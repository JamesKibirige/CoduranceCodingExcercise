using System;
using System.Collections.Generic;

namespace SocialMessenger.Interfaces
{
    public interface ITimeLine
    {
        IDictionary<DateTimeOffset, string> Messages { get; }
        void Add(DateTimeOffset dateTime, string message);
        string ToString(DateTimeOffset dateTime);
    }
}