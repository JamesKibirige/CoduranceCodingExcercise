using Moq;
using SocialMessenger.CommandHandlers;
using SocialMessenger.Interfaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class WallHandlerShould
    {
        [Theory]
        [MemberData(nameof(AggregatedSubscription_Data))]
        public void ProcessCommand_ForExistingUser_VerifyCollaborators(string aggSubscriptions)
        {
            var user = Mock.Of<IUser>();
            Mock.Get(user)
                .Setup(m => m.AggregatedSubscriptions(It.IsAny<DateTimeOffset>(), It.IsAny<ITimeSpanDisplayFormatter>()))
                .Returns(aggSubscriptions);

            var repository = Mock.Of<IUserRepository>();
            Mock.Get(repository)
                .Setup(m => m.HasUser(It.IsAny<string>()))
                .Returns(true);
            Mock.Get(repository)
                .Setup(m => m.GetUser(It.IsAny<string>()))
                .Returns(user);

            new WallHandler
            (
                repository
            ).ProcessCommand("Charlie wall");

            Mock.Get(repository)
                .Verify(m => m.HasUser(It.IsAny<string>()));
            Mock.Get(repository)
                .Verify(m => m.GetUser(It.IsAny<string>()));
            Mock.Get(user)
                .Verify(m => m.AggregatedSubscriptions(It.IsAny<DateTimeOffset>(), It.IsAny<ITimeSpanDisplayFormatter>()));
        }

        [Fact]
        public void ProcessCommand_ForNonExistingUser_VerifyCollaborators()
        {
            var repository = Mock.Of<IUserRepository>();
            Mock.Get(repository)
                .Setup(m => m.HasUser(It.IsAny<string>()))
                .Returns(false);
            Mock.Get(repository)
                .Setup(m => m.GetUser(It.IsAny<string>()));

            new WallHandler
            (
                repository
            ).ProcessCommand("Chloe wall");


            Mock.Get(repository)
                .Verify(m => m.HasUser(It.IsAny<string>()));
            Mock.Get(repository)
                .Verify(m => m.GetUser(It.IsAny<string>()), Times.Never);
        }

        public static IEnumerable<object[]> AggregatedSubscription_Data()
        {
            yield return new object[]
            {
                "Charlie - I\'m in New York today! Anyone want to have a coffee? (2 seconds ago)\r\nAlice - I love the weather today (5 minutes ago)"
            };
        }
    }
}