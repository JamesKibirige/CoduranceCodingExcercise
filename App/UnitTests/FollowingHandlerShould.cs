using Moq;
using SocialMessenger.CommandHandlers;
using SocialMessenger.Interfaces;
using TestUtilities.MockBuilders;
using Xunit;

namespace UnitTests
{
    public class FollowingHandlerShould
    {
        [Fact]
        public void ProcessCommand_ExistingUsers_VerifyCollaborators()
        {
            var user = Mock.Of<IUser>();
            Mock.Get(user)
                .Setup(m => m.SubscribeToTimeLine(It.IsAny<IUser>()));

            var userRepository = new MockUserRepositoryBuilder()
                .WithHasUser(true)
                .WithGetUser(user)
                .Build();

            new FollowingHandler(userRepository).ProcessCommand("James follows Bob");

            Mock.Get(userRepository)
                .Verify(m => m.HasUser(It.IsAny<string>()), Times.Exactly(2));
            Mock.Get(userRepository)
                .Verify(m => m.GetUser(It.IsAny<string>()), Times.Exactly(2));
            Mock.Get(user)
                .Verify(m => m.SubscribeToTimeLine(It.IsAny<IUser>()));
        }

        [Fact]
        public void ProcessCommand_NonExistingUsers_VerifyCollaborators()
        {
            var user = Mock.Of<IUser>();
            Mock.Get(user)
                .Setup(m => m.SubscribeToTimeLine(It.IsAny<IUser>()));

            var userRepository = new MockUserRepositoryBuilder()
                .WithHasUser(false)
                .WithGetUser(user)
                .Build();


            new FollowingHandler(userRepository).ProcessCommand("Sophie follows Stephan");


            Mock.Get(userRepository)
                .Verify(m => m.HasUser(It.IsAny<string>()), Times.AtLeastOnce);
            Mock.Get(user)
                .Verify(m => m.SubscribeToTimeLine(It.IsAny<IUser>()), Times.Never);
        }

        [Fact]
        public void ProcessCommand_ExistingUser_UserFollowsThemselves_VerifyCollaborators()
        {

            var user = Mock.Of<IUser>();
            Mock.Get(user)
                .Setup(m => m.SubscribeToTimeLine(It.IsAny<IUser>()));

            var userRepository = new MockUserRepositoryBuilder()
                .WithHasUser(false)
                .WithGetUser(user)
                .Build();


            new FollowingHandler(userRepository).ProcessCommand("Sophie follows Sophie");


            Mock.Get(userRepository)
                .Verify(m => m.HasUser(It.IsAny<string>()), Times.Never);
            Mock.Get(user)
                .Verify(m => m.SubscribeToTimeLine(It.IsAny<IUser>()), Times.Never);
        }
    }
}