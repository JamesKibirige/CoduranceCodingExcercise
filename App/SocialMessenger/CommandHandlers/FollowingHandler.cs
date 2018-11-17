using SocialMessenger.Interfaces;

namespace SocialMessenger.CommandHandlers
{
    public class FollowingHandler : ICommandHandler
    {
        private readonly IUserRepository _userRepository;
        public FollowingHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void ProcessCommand(string command)
        {
            var commandItems = command.Split(' ');

            if (_userRepository.HasUser(commandItems[0]) && _userRepository.HasUser(commandItems[2]))
            {
                _userRepository
                    .GetUser(commandItems[0])
                    .SubscribeToTimeLine
                    (
                        _userRepository.GetUser(commandItems[2])
                    );

            }
        }
    }
}