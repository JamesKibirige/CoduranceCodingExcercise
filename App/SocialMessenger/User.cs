using SocialMessenger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialMessenger
{
    public class User : IUser
    {
        public string Name { get; }

        public ITimeLine TimeLine { get; protected set; }

        private readonly IDictionary<string, ITimeLine> _subscriptions;

        public User(string name, ITimeLine timeline, IDictionary<string, ITimeLine> subscriptions)
        {
            Name = name;
            TimeLine = timeline;
            _subscriptions = subscriptions;
        }

        public void PublishMessage(DateTimeOffset date, string message)
        {
            TimeLine.Add(date, message);
        }

        public string AggregatedTimeLine(DateTimeOffset currentDateTime, ITimeSpanDisplayFormatter displayFormatter)
        {
            return TimeLine.ToString(currentDateTime, displayFormatter);
        }

        public void SubscribeToTimeLine(IUser aUser)
        {
            if (!_subscriptions.ContainsKey(aUser.Name))
            {
                _subscriptions.Add(aUser.Name, aUser.TimeLine);
            }
        }

        public string AggregatedSubscriptions(DateTimeOffset currentDateTime,
            ITimeSpanDisplayFormatter displayFormatter)
        {
            return
                (
                    from subscription in _subscriptions
                    from message in subscription.Value.Messages
                    select new
                    {
                        UserName = subscription.Key,
                        MessageDateTime = message.Key,
                        Message = message.Value
                    }
                )
                .Union
                (
                    from message in TimeLine.Messages
                    select new
                    {
                        UserName = Name,
                        MessageDateTime = message.Key,
                        Message = message.Value
                    }
                )
                .OrderByDescending(m => m.MessageDateTime)
                .Aggregate
                (
                    string.Empty,
                    (current, item) =>
                        current +
                        $"{item.UserName} - {item.Message} ({displayFormatter.GetFormattedDisplayString(currentDateTime.Subtract(item.MessageDateTime))} ago)\r\n"
                );
        }
    }
}