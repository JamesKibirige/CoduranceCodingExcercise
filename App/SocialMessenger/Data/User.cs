﻿using SocialMessenger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialMessenger.Data
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

        public string AggregatedTimeLine(DateTimeOffset currentDateTime)
        {
            return TimeLine.ToString(currentDateTime);
        }

        public void SubscribeToTimeLine(IUser aUser)
        {
            if (!_subscriptions.ContainsKey(aUser.Name))
            {
                _subscriptions.Add(aUser.Name, aUser.TimeLine);
            }
        }

        public string AggregatedSubscriptions(DateTimeOffset currentDateTime)
        {
            return
            (
                from subscription in _subscriptions
                from message in subscription.Value.Messages
                orderby message.Key descending
                select new
                {
                    UserName = subscription.Key,
                    MessageDateTime = message.Key,
                    Message = message.Value
                }
            ).Aggregate
            (
                string.Empty,
                (current, item) =>
                    current +
                    $"{item.UserName} - {item.Message} ({currentDateTime.Subtract(item.MessageDateTime).TotalMinutes} minutes ago)\r\n"
            );
        }
    }
}