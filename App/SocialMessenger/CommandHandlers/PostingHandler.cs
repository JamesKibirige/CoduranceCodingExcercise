using SocialMessenger.Data;
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

            if (UserRepository.HasUser(userName))
            {
                UserRepository.GetUser(userName)
                    .PublishMessage(DateTimeOffset.Now, command);
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
                                {DateTimeOffset.Now, command}
                            }
                        ),
                        new Dictionary<string, ITimeLine>()
                    )
                );
            }
        }
    }
}