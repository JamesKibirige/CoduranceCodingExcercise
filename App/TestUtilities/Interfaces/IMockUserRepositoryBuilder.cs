using SocialMessenger.Interfaces;
using TestUtilities.MockBuilders;

namespace TestUtilities.Interfaces
{
    public interface IMockUserRepositoryBuilder
    {
        MockUserRepositoryBuilder WithHasUser(bool hasUser);
        MockUserRepositoryBuilder WithGetUser(IUser aUser);
        MockUserRepositoryBuilder WithAddUser();
        IUserRepository Build();
    }
}