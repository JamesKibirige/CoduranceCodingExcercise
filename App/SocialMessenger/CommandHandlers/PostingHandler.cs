using SocialMessenger.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SocialMessenger.CommandHandlers
{
    public class PostingHandler : CommandHandler
    {
        public PostingHandler(IUserRepository userRepository)
            : base(userRepository)
        {
        }
        public override void ProcessCommand(string command)
        {
            var userName = command.Split(' ')[0];
            var messageRegEx = new Regex(Resources.MessageRegEx);
            var message = messageRegEx.Match(command)
                .ToString()
                .Substring(3);

            if (UserRepository.HasUser(userName))
            {
                UserRepository.GetUser(userName)
                    .PublishMessage(DateTimeOffset.Now, message);
            }
            else
            {
                UserRepository.AddUser
                (
                    new User
                    (
                        userName,
                        new TimeLine
                        (
                            new Dictionary<DateTimeOffset, string>()
                            {
                                {DateTimeOffset.Now, message}
                            }
                        ),
                        new Dictionary<string, ITimeLine>()
                    )
                );
            }
        }
    }
}