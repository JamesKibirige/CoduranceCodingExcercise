using SocialMessenger.Data;
using SocialMessenger.Interfaces;
using System.Collections.Generic;

namespace SocialMessenger.CommandHandlers
{
    public class PostingHandler : ICommandHandler
    {
        private readonly IUserRepository _userRepository;
        public PostingHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public void ProcessCommand(string command)
        {
            var userName = command.Split(' ')[0];

            if (_userRepository.HasUser(userName))
            {
                _userRepository.GetUser(userName)
                    .PublishMessage(command);
            }
            else
            {
                _userRepository.AddUser
                (
                    new User
                    (
                        userName,
                        new List<string>()
                        {
                            command
                        }
                    )
                );
            }
        }
    }
}