using SocialMessenger.Interfaces;
using System;

namespace SocialMessenger.CommandHandlers
{
    public class ReadingHandler : ICommandHandler
    {
        private readonly IUserRepository _userRepository;

        public ReadingHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void ProcessCommand(string command)
        {
            if (_userRepository.HasUser(command))
            {
                //If user exists Output users current timeline to the Console
                Console.Write
                (
                    _userRepository.GetUser(command)
                        .AggregatedTimeLine(DateTimeOffset.Now)
                );
            }
        }
    }
}