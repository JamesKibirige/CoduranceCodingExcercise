using SocialMessenger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialMessenger.Data
{
    public class TimeLine : ITimeLine
    {
        private readonly IDictionary<DateTimeOffset, string> _messages;

        public TimeLine(IDictionary<DateTimeOffset, string> messages)
        {
            _messages = messages;
        }

        public void Add(DateTimeOffset dateTime, string message)
        {
            _messages.Add(dateTime, message);
        }

        public string ToString(DateTimeOffset dateTime)
        {
            return _messages
                .OrderByDescending(t => t.Key)
                .Aggregate
                (
                    string.Empty,
                    (current, item) => current + $"{item.Value} ({dateTime.Subtract(item.Key).TotalMinutes} minutes ago)\r\n"
                );
        }
    }
}