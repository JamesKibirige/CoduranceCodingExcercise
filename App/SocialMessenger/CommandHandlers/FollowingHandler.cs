using SocialMessenger.Interfaces;

namespace SocialMessenger.CommandHandlers
{
    public class FollowingHandler : CommandHandler
    {
        public FollowingHandler(IUserRepository userRepository)
            : base(userRepository)
        {
        }

        public override void ProcessCommand(string command)
        {
            var commandItems = command.Split(' ');

            if
            (
                commandItems[0] != commandItems[2]
                && UserRepository.HasUser(commandItems[0])
                && UserRepository.HasUser(commandItems[2])
            )
            {
                UserRepository
                    .GetUser(commandItems[0])
                    .SubscribeToTimeLine
                    (
                        UserRepository.GetUser(commandItems[2])
                    );

            }
        }
    }
}