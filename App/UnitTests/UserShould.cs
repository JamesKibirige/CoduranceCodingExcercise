using Moq;
using SocialMessenger.Data;
using SocialMessenger.Interfaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class UserShould
    {
        [Fact]
        public void PublishMessage_ToPersonalTimeLine_VerifyCollaborators()
        {
            //Arrange
            var timeline = Mock.Of<ITimeLine>();

            //Act
            new User("James", timeline, Mock.Of<IDictionary<string, ITimeLine>>())
                .PublishMessage
                (
                    new DateTimeOffset(2018, 11, 17, 1, 23, 30, TimeSpan.Zero),
                    "Hello!"
                );

            //Assert
            Mock.Get(timeline)
                .Verify(m => m.Add(It.IsAny<DateTimeOffset>(), It.IsAny<string>()));
        }

        [Theory]
        [MemberData(nameof(UserName_Data))]
        public void Name_ReturnsUserName(string username)
        {
            //Arrange
            var user = new User
            (
                username,
                Mock.Of<ITimeLine>(),
                Mock.Of<IDictionary<string, ITimeLine>>()
            );

            //Act
            //Assert
            Assert.Equal(username, user.Name);
        }

        [Theory]
        [MemberData(nameof(TimeLine_Data))]
        public void AggregatedTimeLine_ReturnsAggregatedTimeLine(ITimeLine timeline)
        {
            //Arrange
            var user = new User
            (
                "James",
                timeline,
                Mock.Of<IDictionary<string, ITimeLine>>()
            );

            //Act
            var result = user.AggregatedTimeLine
            (
                new DateTimeOffset(2018, 11, 16, 16, 30, 0, TimeSpan.Zero)
            );

            //Assert
            Assert.NotNull(result);
            Assert.Equal(95, result.Length);
        }

        [Fact]
        public void SubscribeToTimeLine_NonExistingSubscription_VerifyCollaborators()
        {
            //Arrange
            var subscriptions = Mock.Of<IDictionary<string, ITimeLine>>();
            Mock.Get(subscriptions)
                .Setup(m => m.ContainsKey(It.IsAny<string>()))
                .Returns(false);
            Mock.Get(subscriptions)
                .Setup(m => m.Add(It.IsAny<string>(), It.IsAny<ITimeLine>()));

            var anotherUser = Mock.Of<IUser>();
            Mock.Get(anotherUser)
                .Setup(m => m.Name)
                .Returns("Sophie");
            Mock.Get(anotherUser)
                .Setup(m => m.TimeLine);

            //Act
            new User("James", Mock.Of<ITimeLine>(), subscriptions)
                .SubscribeToTimeLine(anotherUser);

            //Assert
            Mock.Get(subscriptions)
                .Verify(m => m.ContainsKey(It.IsAny<string>()));
            Mock.Get(subscriptions)
                .Verify(m => m.Add(It.IsAny<string>(), It.IsAny<ITimeLine>()));
            Mock.Get(anotherUser)
                .Verify(m => m.Name);
            Mock.Get(anotherUser)
                .Verify(m => m.TimeLine);
        }

        [Fact]
        public void SubscribeToTimeLine_ExistingSubscription_VerifyCollaborators()
        {
            //Arrange
            var subscriptions = Mock.Of<IDictionary<string, ITimeLine>>();
            Mock.Get(subscriptions)
                .Setup(m => m.ContainsKey(It.IsAny<string>()))
                .Returns(true);

            var anotherUser = Mock.Of<IUser>();
            Mock.Get(anotherUser)
                .Setup(m => m.Name)
                .Returns("Sophie");

            //Act
            new User("James", Mock.Of<ITimeLine>(), subscriptions)
                .SubscribeToTimeLine(anotherUser);

            //Assert
            Mock.Get(subscriptions)
                .Verify(m => m.ContainsKey(It.IsAny<string>()));
            Mock.Get(subscriptions)
                .Verify(m => m.Add(It.IsAny<string>(), It.IsAny<ITimeLine>()), Times.Never);
            Mock.Get(anotherUser)
                .Verify(m => m.Name);
        }

        public static IEnumerable<object[]> UserName_Data()
        {
            yield return new object[] { "James" };
            yield return new object[] { "Kato" };
            yield return new object[] { "Sam" };
            yield return new object[] { "Waswa" };
            yield return new object[] { "Kasule" };
        }

        public static IEnumerable<object[]> TimeLine_Data()
        {
            yield return new object[]
            {
                new TimeLine
                (
                    new Dictionary<DateTimeOffset, string>()
                    {
                        {
                            new DateTimeOffset(2018, 11, 16, 16, 10, 0, TimeSpan.Zero),
                            "Hello World"
                        },
                        {
                            new DateTimeOffset(2018, 11, 16, 16, 15, 0, TimeSpan.Zero),
                            "My name is James"
                        },
                        {
                            new DateTimeOffset(2018, 11, 16, 16, 25, 0, TimeSpan.Zero),
                            "I am Ugandan"
                        },
                    }
                )
            };
        }
    }
}
