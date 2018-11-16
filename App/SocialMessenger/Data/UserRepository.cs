using SocialMessenger.Interfaces;
using System.Collections.Generic;

namespace SocialMessenger.Data
{
    /// <summary>
    /// TODO: UserRepository Unit tests
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly IDictionary<string, IUser> _userRepository;

        public UserRepository(IDictionary<string, IUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public bool HasUser(string username)
        {
            return _userRepository.ContainsKey(username);
        }

        public void AddUser(IUser user)
        {
            _userRepository.Add(user.Name, user);
        }

        public IUser GetUser(string username)
        {
            return _userRepository[username];
        }
    }
}