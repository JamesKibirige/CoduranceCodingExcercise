using SocialMessenger.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SocialMessenger.Data
{
    public class User : IUser
    {
        public string Name { get; }
        public IEnumerable<string> TimeLine { get; protected set; }

        public User(string name, IEnumerable<string> timeline)
        {
            Name = name;
            TimeLine = timeline;
        }

        public void PublishMessage(string message)
        {
            var timeline = TimeLine.ToList();
            timeline.Add(message);
            TimeLine = timeline;
        }
    }
}