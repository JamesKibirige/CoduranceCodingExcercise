using Microsoft.Extensions.Configuration;
using SocialMessenger;
using SocialMessenger.CommandHandlers;
using SocialMessenger.Configurations;
using SocialMessenger.Data;
using SocialMessenger.Enumerations;
using SocialMessenger.Interfaces;
using SocialMessenger.Options;
using System;
using System.Collections.Generic;
using TestUtilities.TestData;
using Xunit;
namespace IntegrationTests
{
    public class CommandHandlerFactoryShould
    {
        [Theory]
        [MemberData(nameof(PostingCommand_Data))]
        public void GetHandler_ForPostingCommand_UsingTestData_ReturnsHandler(string command)
        {
            //Arrange
            //Act
            var handler = new CommandHandlerFactory
            (
                MappingTestData.Main.Data
            ).GetHandler(command);

            //Assert
            Assert.NotNull(handler);
            Assert.IsAssignableFrom<PostingHandler>(handler);
        }

        [Theory]
        [MemberData(nameof(Command_Handler_Data))]
        public void GetHandler_UsingConfigurationData_ReturnsHandler(string command, Type commandHandlerType)
        {
            //Arrange
            //Act
            var handler = new CommandHandlerFactory
            (
                new CommandHandlerMappingFactory
                    (
                        Configuration
                            .Development
                            .ConfigurationRoot
                            .GetSection(ConfigurationKey.CommandHandlerMappings)
                            .Get<IEnumerable<CommandHandlerMappingOptions>>(),
                        new UserRepository
                        (
                            new Dictionary<string, IUser>()
                        )
                    )
                    .CreateMappings()
            ).GetHandler(command);

            //Assert
            Assert.NotNull(handler);
            Assert.IsAssignableFrom(commandHandlerType, handler);
        }

        public static IEnumerable<object[]> Command_Handler_Data()
        {
            yield return new object[] { "Alice -> I love the weather today", typeof(PostingHandler) };
            yield return new object[] { "Bob -> Damn! We lost!", typeof(PostingHandler) };
            yield return new object[] { "Bob -> Good game though.", typeof(PostingHandler) };
            yield return new object[] { "Charlie -> I'm in New York today! Anyone want to have a coffee?", typeof(PostingHandler) };
            yield return new object[] { "Charlie follows Bob", typeof(FollowingHandler) };
            yield return new object[] { "James follows Kato", typeof(FollowingHandler) };
            yield return new object[] { "Alice", typeof(ReadingHandler) };
            yield return new object[] { "Wasswa", typeof(ReadingHandler) };
            yield return new object[] { "Charlie wall", typeof(WallHandler) };
            yield return new object[] { "Kisuule wall", typeof(WallHandler) };
            yield return new object[] { "Kalule wall", typeof(WallHandler) };
        }

        public static IEnumerable<object[]> PostingCommand_Data()
        {
            yield return new object[] { "James -> Hello my name is James" };
            yield return new object[] { "Alice -> I love the weather today" };
            yield return new object[] { "Bob -> Damn! We lost!" };
            yield return new object[] { "Bob -> Good game though." };
            yield return new object[] { "Charlie -> I'm in New York today! Anyone want to have a coffee?" };
        }
    }
}