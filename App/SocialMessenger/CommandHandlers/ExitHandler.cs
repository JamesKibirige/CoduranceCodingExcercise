using SocialMessenger.Interfaces;
using System;

namespace SocialMessenger.CommandHandlers
{
    public class ExitHandler : CommandHandler
    {
        public ExitHandler(IUserRepository userRepository) : base(userRepository)
        {
        }
        public override void ProcessCommand(string command)
        {
            Console.WriteLine($"Processing Command: {command}, exiting application...");
            Environment.Exit(0);
        }
    }
}