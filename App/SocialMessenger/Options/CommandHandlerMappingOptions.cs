namespace SocialMessenger.Options
{
    public class CommandHandlerMappingOptions
    {
        public string RegEx { get; set; }
        public string CommandHandler { get; set; }

        public CommandHandlerMappingOptions()
        {
        }
        public CommandHandlerMappingOptions(string regEx, string commandHandler)
        {
            RegEx = regEx;
            CommandHandler = commandHandler;
        }
    }
}