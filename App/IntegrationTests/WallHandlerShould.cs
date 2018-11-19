using FluentAssertions;
using SocialMessenger;
using SocialMessenger.CommandHandlers;
using SocialMessenger.Data;
using SocialMessenger.Interfaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace IntegrationTests
{
    public class WallHandlerShould
    {
        [Fact]
        public void ProcessCommand_ForExistingUsers_ViewAllSubscriptions()
        {
            //Arrange
            var user = new User
            (
                "Charlie",
                new TimeLine
                (
                    new Dictionary<DateTimeOffset, string>()
                ),
                new Dictionary<string, ITimeLine>()
                {
                    {
                        "Sophie",
                        new TimeLine
                        (
                            new Dictionary<DateTimeOffset, string>()
                            {
                                {
                                    new DateTimeOffset(2018,11,16,0,0,0,TimeSpan.Zero),
                                    "Hi my name is Sophie!"
                                },
                                {
                                    new DateTimeOffset(2018,11,16,0,5,0,TimeSpan.Zero),
                                    "I live in London!"
                                },
                                {
                                    new DateTimeOffset(2018,11,16,0,10,0,TimeSpan.Zero),
                                    "I am 27 years old and female..."
                                },
                                {
                                    new DateTimeOffset(2018,11,16,0,15,0,TimeSpan.Zero),
                                    "I love art and craft!"
                                }
                            }
                        )
                    }
                }
            );

            var user1 = new User
            (
                "Sophie",
                new TimeLine
                (
                    new Dictionary<DateTimeOffset, string>()
                ),
                new Dictionary<string, ITimeLine>()
            );

            //Act
            Action processCommand = () => new WallHandler
            (
                new UserRepository
                (
                    new Dictionary<string, IUser>()
                    {
                        {
                            user.Name,
                            user
                        },
                        {
                            user1.Name,
                            user1
                        },
                    }
                )
            ).ProcessCommand("Charlie wall");

            //Assert
            processCommand.Should().NotThrow();
        }
    }
}