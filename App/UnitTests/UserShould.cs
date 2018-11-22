using Moq;
using SocialMessenger;
using SocialMessenger.Interfaces;
using SocialMessenger.Utilities;
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
            var timeline = Mock.Of<ITimeLine>();

            new User("James", timeline, Mock.Of<IDictionary<string, ITimeLine>>())
                .PublishMessage
                (
                    new DateTimeOffset(2018, 11, 17, 1, 23, 30, TimeSpan.Zero),
                    "Hello!"
                );

            Mock.Get(timeline)
                .Verify(m => m.Add(It.IsAny<DateTimeOffset>(), It.IsAny<string>()));
        }

        [Theory]
        [MemberData(nameof(UserName_Data))]
        public void Name_ReturnsUserName(string username)
        {
            var user = new User
            (
                username,
                Mock.Of<ITimeLine>(),
                Mock.Of<IDictionary<string, ITimeLine>>()
            );

            Assert.Equal(username, user.Name);
        }

        [Theory]
        [MemberData(nameof(TimeLine_Data))]
        public void AggregatedTimeLine_ReturnsAggregatedTimeLine(ITimeLine timeline)
        {
            var user = new User
            (
                "James",
                timeline,
                Mock.Of<IDictionary<string, ITimeLine>>()
            );

            var result = user.AggregatedTimeLine
            (
                new DateTimeOffset(2018, 11, 16, 16, 30, 0, TimeSpan.Zero),
                new TimeSpanDisplayFormatter()
            );


            Assert.NotNull(result);
            Assert.Equal(98, result.Length);
        }

        [Fact]
        public void SubscribeToTimeLine_NonExistingSubscription_VerifyCollaborators()
        {
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

            new User("James", Mock.Of<ITimeLine>(), subscriptions)
                .SubscribeToTimeLine(anotherUser);

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
            var subscriptions = Mock.Of<IDictionary<string, ITimeLine>>();
            Mock.Get(subscriptions)
                .Setup(m => m.ContainsKey(It.IsAny<string>()))
                .Returns(true);

            var anotherUser = Mock.Of<IUser>();
            Mock.Get(anotherUser)
                .Setup(m => m.Name)
                .Returns("Sophie");

            new User("James", Mock.Of<ITimeLine>(), subscriptions)
                .SubscribeToTimeLine(anotherUser);

            Mock.Get(subscriptions)
                .Verify(m => m.ContainsKey(It.IsAny<string>()));
            Mock.Get(subscriptions)
                .Verify(m => m.Add(It.IsAny<string>(), It.IsAny<ITimeLine>()), Times.Never);
            Mock.Get(anotherUser)
                .Verify(m => m.Name);
        }

        [Theory]
        [MemberData(nameof(Subscription_Data))]
        public void AggregatedSubscriptions_ReturnsAggregatedTimeLineSubscriptions(IDictionary<string, ITimeLine> subscriptions)
        {
            var user = new User
            (
                "James",
                new TimeLine
                (
                    new Dictionary<DateTimeOffset, string>()
                ),
                subscriptions
            );


            var result = user.AggregatedSubscriptions
            (
                new DateTimeOffset(2018, 11, 16, 17, 05, 0, TimeSpan.Zero),
                new TimeSpanDisplayFormatter()
            );

            Assert.NotNull(result);
            Assert.Equal(466, result.Length);
        }

        public static IEnumerable<object[]> UserName_Data()
        {
            yield return new object[] { "James" };
            yield return new object[] { "Kato" };
            yield return new object[] { "Sam" };
            yield return new object[] { "Waswa" };
            yield return new object[] { "Kasule" };
        }

        public static IEnumerable<object[]> Subscription_Data()
        {
            yield return new object[]
            {
                new Dictionary<string, ITimeLine>()
                {
                    {
                        "James",
                        new TimeLine
                        (
                            new Dictionary<DateTimeOffset, string>()
                            {
                                {
                                    new DateTimeOffset(2018, 11, 16, 16, 00, 0, TimeSpan.Zero),
                                    "Hello my name is James"
                                },
                                {
                                    new DateTimeOffset(2018, 11, 16, 16, 5, 0, TimeSpan.Zero),
                                    "I live in London but have origins in Uganda"
                                },
                                {
                                    new DateTimeOffset(2018, 11, 16, 16, 10, 0, TimeSpan.Zero),
                                    "I am a proud black man"
                                }
                            }
                        )
                    },
                    {
                        "Katie",
                        new TimeLine
                        (
                            new Dictionary<DateTimeOffset, string>()
                            {
                                {
                                    new DateTimeOffset(2018, 11, 16, 16, 30, 0, TimeSpan.Zero),
                                    "Hello my name is Katie"
                                },
                                {
                                    new DateTimeOffset(2018, 11, 16, 16, 35, 0, TimeSpan.Zero),
                                    "I live in East London"
                                },
                                {
                                    new DateTimeOffset(2018, 11, 16, 16, 40, 0, TimeSpan.Zero),
                                    "I am a beautiful black woman"
                                }
                            }
                        )
                    },
                    {
                        "Amy",
                        new TimeLine
                        (
                            new Dictionary<DateTimeOffset, string>()
                            {
                                {
                                    new DateTimeOffset(2018, 11, 16, 16, 42, 0, TimeSpan.Zero),
                                    "Hello my name is Amy"
                                },
                                {
                                    new DateTimeOffset(2018, 11, 16, 16, 48, 0, TimeSpan.Zero),
                                    "I live in Shepard's Bush"
                                },
                                {
                                    new DateTimeOffset(2018, 11, 16, 16, 52, 0, TimeSpan.Zero),
                                    "I am a teacher"
                                }
                            }
                        )
                    }
                }
            };
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
