using Moq;
using SocialMessenger.CommandHandlers;
using SocialMessenger.Data;
using SocialMessenger.Interfaces;
using System.Collections.Generic;
using Xunit;
namespace UnitTests
{
    public class PostingHandlerShould
    {
        [Fact]
        public void ProcessCommand_ExistingUser_VerifyCollaborators()
        {
            //Arrange
            var userRepository = Mock.Of<IUserRepository>();
            Mock.Get(userRepository)
                .Setup(m => m.HasUser(It.IsAny<string>()))
                .Returns(true);
            Mock.Get(userRepository)
                .Setup(m => m.GetUser(It.IsAny<string>()))
                .Returns
                (
                    new User("TestUser", new List<string>())
                );

            //Act
            new PostingHandler(userRepository).ProcessCommand("James -> Hello, Hello");

            //Assert
            Mock.Get(userRepository)
                .Verify(m => m.HasUser(It.IsAny<string>()));
            Mock.Get(userRepository)
                .Verify(m => m.GetUser(It.IsAny<string>()));
        }

        [Fact]
        public void ProcessCommand_ExistingUser_PublishesCommandToUserTimeLine()
        {
            //Arrange
            var user = new User("TestUser", new List<string>());
            var userRepository = Mock.Of<IUserRepository>();
            Mock.Get(userRepository)
                .Setup(m => m.HasUser(It.IsAny<string>()))
                .Returns(true);
            Mock.Get(userRepository)
                .Setup(m => m.GetUser(It.IsAny<string>()))
                .Returns
                (
                    user
                );

            //Act
            new PostingHandler(userRepository).ProcessCommand("James -> Hello, Hello");

            //Assert
            Assert.NotEmpty(user.TimeLine);
        }

        [Fact]
        public void ProcessCommand_NonExistingUser_VerifyCollaborators()
        {
            //Arrange
            var userRepository = Mock.Of<IUserRepository>();
            Mock.Get(userRepository)
                .Setup(m => m.HasUser(It.IsAny<string>()))
                .Returns(false);
            Mock.Get(userRepository)
                .Setup(m => m.AddUser(It.IsAny<IUser>()));

            //Act
            new PostingHandler(userRepository).ProcessCommand("James -> Hello, Hello");

            //Assert
            Mock.Get(userRepository)
                .Verify(m => m.HasUser(It.IsAny<string>()));
            Mock.Get(userRepository)
                .Verify(m => m.AddUser(It.IsAny<IUser>()));
        }
    }
}