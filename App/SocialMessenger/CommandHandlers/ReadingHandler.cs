using SocialMessenger.Interfaces;
using SocialMessenger.Utilities;
using System;

namespace SocialMessenger.CommandHandlers
{
    public class ReadingHandler : CommandHandler
    {
        public ReadingHandler(IUserRepository userRepository)
            : base(userRepository)
        {
        }
        public override void ProcessCommand(string command)
        {
            if (UserRepository.HasUser(command))
            {
                Console.Write
                (
                    UserRepository.GetUser(command)
                        .AggregatedTimeLine
                        (
                            DateTimeOffset.Now,
                            new TimeSpanDisplayFormatter()
                        )
                );
            }
        }
    }
}