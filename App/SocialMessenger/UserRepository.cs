using SocialMessenger.Interfaces;
using System.Collections.Generic;

namespace SocialMessenger
{
    public class UserRepository : IUserRepository
    {
        private readonly IDictionary<string, IUser> _users;

        public UserRepository(IDictionary<string, IUser> users)
        {
            _users = users;
        }

        public bool HasUser(string username)
        {
            return _users.ContainsKey(username);
        }

        public void AddUser(IUser user)
        {
            _users.Add(user.Name, user);
        }

        public IUser GetUser(string username)
        {
            return _users[username];
        }
    }
}