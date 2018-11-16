namespace SocialMessenger.Interfaces
{
    public interface IUser
    {
        void PublishMessage(string message);
        string Name { get; }
    }
}