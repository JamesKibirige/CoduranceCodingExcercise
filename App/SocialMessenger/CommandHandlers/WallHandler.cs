using SocialMessenger.Interfaces;
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
                //If wall - If user exists Output aggregated timeline
                Console.Write
                (
                    UserRepository.GetUser(command)
                        .AggregatedSubscriptions(DateTimeOffset.Now)
                );
            }
        }
    }
}