using SocialMessenger.Data;
using SocialMessenger.Enumerations;
using SocialMessenger.Interfaces;
using System;
using System.Collections.Generic;

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
            var message = RegularExpressions
                .Message
                .RegEx
                .Match(command)
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