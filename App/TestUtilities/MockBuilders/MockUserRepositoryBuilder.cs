using Moq;
using SocialMessenger.Interfaces;
using TestUtilities.Interfaces;

namespace TestUtilities.MockBuilders
{
    public class MockUserRepositoryBuilder : IMockUserRepositoryBuilder
    {
        private readonly IUserRepository _userRepository = Mock.Of<IUserRepository>();

        public MockUserRepositoryBuilder WithHasUser(bool hasUser)
        {
            Mock.Get(_userRepository)
                .Setup(m => m.HasUser(It.IsAny<string>()))
                .Returns(hasUser);

            return this;
        }

        public MockUserRepositoryBuilder WithGetUser(IUser aUser)
        {
            Mock.Get(_userRepository)
                .Setup(m => m.GetUser(It.IsAny<string>()))
                .Returns(aUser);

            return this;
        }

        public MockUserRepositoryBuilder WithAddUser()
        {
            Mock.Get(_userRepository)
                .Setup(m => m.AddUser(It.IsAny<IUser>()));

            return this;
        }

        public IUserRepository Build()
        {
            return _userRepository;
        }
    }
}