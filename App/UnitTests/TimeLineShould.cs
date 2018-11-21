﻿using Moq;
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
        public void Add_NewTimeLineMessage_MessageAdded()
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
        public void ToString_ReturnAggregatedTimeLine()
        {
            var timeline = new TimeLine
            (
                new Dictionary<DateTimeOffset, string>()
                {
                    {
                        new DateTimeOffset(2018, 11, 17, 0, 10, 30, TimeSpan.Zero),
                        "Hello!"
                    }
                }
            );

            var result = timeline.ToString
            (
                new DateTimeOffset(2018, 11, 17, 0, 12, 30, TimeSpan.Zero),
                new TimeSpanDisplayFormatter()
            );

            Assert.NotNull(result);
            Assert.Equal(25, result.Length);
        }
    }
}