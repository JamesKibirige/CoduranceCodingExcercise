using SocialMessenger.Interfaces;
using SocialMessenger.Utilities;
using System;

namespace SocialMessenger.CommandHandlers
{
    public class WallHandler : CommandHandler
    {
        public WallHandler(IUserRepository userRepository)
            : base(userRepository)
        {
        }

        public override void ProcessCommand(string command)
        {
            var username = command.Split(' ')[0];

            if (UserRepository.HasUser(username))
            {
                Console.Write
                (
                    UserRepository.GetUser(username)
                        .AggregatedSubscriptions
                        (
                            DateTimeOffset.Now,
                            new TimeSpanDisplayFormatter()
                        )
                );
            }
        }
    }
}