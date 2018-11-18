namespace SocialMessenger.Enumerations
{
    public abstract class ConfigurationKey : Enumeration
    {
        public static readonly ConfigurationKey CommandHandlerMappings = new CommandHandlerMappingsConfigurationKey();

        private ConfigurationKey(string name, int value)
            : base(name, value)
        {
        }

        private class CommandHandlerMappingsConfigurationKey : ConfigurationKey
        {
            public CommandHandlerMappingsConfigurationKey() : base("CommandHandlerMappings:CommandHandlerMappingOptions", 1)
            {
            }
        }
    }
}