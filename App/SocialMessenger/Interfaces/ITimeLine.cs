using System;

namespace SocialMessenger.Interfaces
{
    public interface ITimeLine
    {
        void Add(DateTimeOffset dateTime, string message);
        string ToString(DateTimeOffset dateTime);
    }
}