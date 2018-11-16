namespace SocialMessenger.Interfaces
{
    public interface IUserRepository
    {
        bool HasUser(string username);
        void AddUser(IUser user);
        IUser GetUser(string username);
    }
}