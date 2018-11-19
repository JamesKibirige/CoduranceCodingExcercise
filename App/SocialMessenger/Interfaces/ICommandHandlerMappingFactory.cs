using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SocialMessenger.Interfaces
{
    public interface ICommandHandlerMappingFactory
    {
        IDictionary<Regex, ICommandHandler> CreateMappings();
    }
}