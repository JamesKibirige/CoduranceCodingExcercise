using SocialMessenger.Interfaces;

namespace SocialMessenger.CommandHandlers
{
    public abstract class CommandHandler : ICommandHandler
    {
        protected readonly IUserRepository UserRepository;

        protected CommandHandler(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public abstract void ProcessCommand(string command);
    }
}