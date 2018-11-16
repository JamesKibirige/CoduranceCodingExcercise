using CoduranceSocialMessenger.Interfaces;
using SocialMessenger.Interfaces;
using System.Collections.Generic;
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
            ICommandHandler result = null;

            foreach (var mapping in _commandHandlerMappings)
            {
                if (mapping.Key.IsMatch(command))
                {
                    result = mapping.Value;
                }
            }

            return result;
        }
    }
}