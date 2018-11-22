using FluentAssertions;
using Moq;
using SocialMessenger;
using SocialMessenger.Utilities;
using System;
using System.Collections.Generic;
using Xunit;
namespace UnitTests
{
    public class TimeLineShould
    {
        [Fact]
        public void Add_NewTimeLineMessage_VerifyCollaborators()
        {
            var messages = Mock.Of<IDictionary<DateTimeOffset, string>>();
            Mock.Get(messages)
                .Setup
                (
                    m => m.Add(It.IsAny<DateTimeOffset>(), It.IsAny<string>())
                );

            new TimeLine(messages).Add
            (
                new DateTimeOffset(2018, 11, 17, 0, 10, 30, TimeSpan.Zero),
                "Hello!"
            );

            Mock.Get(messages)
                .Verify
                (
                    m => m.Add(It.IsAny<DateTimeOffset>(), It.IsAny<string>())
                );
        }

        [Fact]
        public void Add_NewTimeLineMessage_NewMessageAdded()
        {
            var timeline = new TimeLine
            (
                new Dictionary<DateTimeOffset, string>()
            );

            timeline.Add
            (
                new DateTimeOffset(2018, 11, 17, 0, 10, 30, TimeSpan.Zero),
                "Hello!"
            );

            timeline
                .Messages
                .Should()
                .Contain
                (
                    new DateTimeOffset(2018, 11, 17, 0, 10, 30, TimeSpan.Zero),
                    "Hello!"
                );
        }

        [Fact]
        public void ToString_ReturnAggregatedTimeLine()
        {
            var result = new TimeLine
            (
                new Dictionary<DateTimeOffset, string>()
                {
                    {
                        new DateTimeOffset(2018, 11, 17, 0, 10, 30, TimeSpan.Zero),
                        "Hello!"
                    }
                }
            ).ToString
            (
                new DateTimeOffset(2018, 11, 17, 0, 12, 30, TimeSpan.Zero),
                new TimeSpanDisplayFormatter()
            );

            Assert.NotNull(result);
            result.Should().BeEquivalentTo("Hello! (2 minutes  ago)\r\n");
        }
    }
}