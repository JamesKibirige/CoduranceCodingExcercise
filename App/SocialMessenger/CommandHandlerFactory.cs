using SocialMessenger.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SocialMessenger
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IDictionary<Regex, ICommandHandler> _commandHandlerMappings;

        public CommandHandlerFactory(IDictionary<Regex, ICommandHandler> commandHandlerMappings)
        {
            _commandHandlerMappings = commandHandlerMappings;
        }

        public ICommandHandler GetHandler(string command)
        {
            return _commandHandlerMappings
                .FirstOrDefault(m => m.Key.IsMatch(command))
                .Value;
        }
    }
}