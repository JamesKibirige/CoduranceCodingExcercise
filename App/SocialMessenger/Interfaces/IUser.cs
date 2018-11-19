using System;

namespace SocialMessenger.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        ITimeLine TimeLine { get; }
        void PublishMessage(DateTimeOffset date, string message);
        string AggregatedTimeLine(DateTimeOffset currentDateTime, ITimeSpanDisplayFormatter displayFormatter);
        void SubscribeToTimeLine(IUser aUser);
        string AggregatedSubscriptions(DateTimeOffset currentDateTime, ITimeSpanDisplayFormatter displayFormatter);
    }
}