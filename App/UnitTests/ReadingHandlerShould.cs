using Moq;
using SocialMessenger.CommandHandlers;
using SocialMessenger.Interfaces;
using System;
using TestUtilities.MockBuilders;
using Xunit;

namespace UnitTests
{
    public class ReadingHandlerShould
    {
        [Fact]
        public void ProcessCommand_ExistingUser_VerifyCollaborators()
        {
            var user = Mock.Of<IUser>();
            Mock.Get(user)
                .Setup(m => m.AggregatedTimeLine(It.IsAny<DateTimeOffset>(), It.IsAny<ITimeSpanDisplayFormatter>()))
                .Returns("> James\r\nHello World. (1 minute ago)\r\nMy name is James. (3 minute ago)\r\nI am Ugandan. (4 minute ago)");

            var userRepository = new MockUserRepositoryBuilder()
                .WithHasUser(true)
                .WithGetUser(user)
                .Build();

            new ReadingHandler(userRepository).ProcessCommand("James");

            Mock.Get(userRepository)
                .Verify(m => m.HasUser(It.IsAny<string>()));
            Mock.Get(userRepository)
                .Verify(m => m.GetUser(It.IsAny<string>()));
            Mock.Get(user)
                .Verify(m => m.AggregatedTimeLine(It.IsAny<DateTimeOffset>(), It.IsAny<ITimeSpanDisplayFormatter>()));
        }

        [Fact]
        public void ProcessCommand_NonExistingUser_VerifyCollaborators()
        {
            var userRepository = new MockUserRepositoryBuilder()
                .WithHasUser(false)
                .Build();

            new ReadingHandler(userRepository).ProcessCommand("James");

            Mock.Get(userRepository)
                .Verify(m => m.HasUser(It.IsAny<string>()));
            Mock.Get(userRepository)
                .Verify(m => m.GetUser(It.IsAny<string>()), Times.Never);
        }
    }
}