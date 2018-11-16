using CoduranceSocialMessenger.Interfaces;

namespace SocialMessenger.Interfaces
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler GetHandler(string command);
    }
}