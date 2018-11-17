using System;
using System.Collections.Generic;

namespace SocialMessenger.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        ITimeLine TimeLine { get; }
        void PublishMessage(DateTimeOffset date, string message);
        string AggregatedTimeLine(DateTimeOffset currentDateTime);
        void SubscribeToTimeLine(IUser aUser);
    }
}