using SocialMessenger.Interfaces;
using SocialMessenger.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SocialMessenger.Factories
{
    public class CommandHandlerMappingFactory : ICommandHandlerMappingFactory
    {
        private readonly IEnumerable<CommandHandlerMappingOptions> _options;
        private readonly IUserRepository _repository;
        public CommandHandlerMappingFactory(IEnumerable<CommandHandlerMappingOptions> options, IUserRepository repository)
        {
            _options = options;
            _repository = repository;
        }

        public IDictionary<Regex, ICommandHandler> CreateMappings()
        {
            return _options
                .Where(o => o.RegEx != string.Empty)
                .ToDictionary
                (
                    option => new Regex(option.RegEx),
                    option => (ICommandHandler)Activator.CreateInstance
                    (
                        Type.GetType(option.CommandHandler),
                        _repository
                    )
                );
        }
    }
}