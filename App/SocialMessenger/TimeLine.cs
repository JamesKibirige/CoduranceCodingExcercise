using SocialMessenger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialMessenger
{
    public class TimeLine : ITimeLine
    {
        public IDictionary<DateTimeOffset, string> Messages { get; }

        public TimeLine(IDictionary<DateTimeOffset, string> messages)
        {
            Messages = messages;
        }

        public void Add(DateTimeOffset dateTime, string message)
        {
            Messages.Add(dateTime, message);
        }

        public string ToString(DateTimeOffset dateTime, ITimeSpanDisplayFormatter displayFormatter)
        {
            return Messages
                .OrderByDescending(t => t.Key)
                .Aggregate
                (
                    string.Empty,
                    (current, item) => current + $"{item.Value} ({displayFormatter.GetFormattedDisplayString(dateTime.Subtract(item.Key))} ago)\r\n"
                );
        }
    }
}