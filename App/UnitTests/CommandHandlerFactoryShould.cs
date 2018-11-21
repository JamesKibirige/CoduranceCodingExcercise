using SocialMessenger.CommandHandlers;
using SocialMessenger.Factories;
using System;
using System.Collections.Generic;
using TestUtilities.TestData;
using Xunit;
namespace UnitTests
{
    public class CommandHandlerFactoryShould
    {
        [Theory]
        [MemberData(nameof(Command_Handler_Data))]
        public void GetHandler_ForExistantCommand_ReturnsHandler(string command, Type commandHandlerType)
        {
            //Arrange
            //Act
            var handler = new CommandHandlerFactory
            (
                MappingTestData.Main.Data
            ).GetHandler(command);

            //Assert
            Assert.NotNull(handler);
            Assert.IsType(commandHandlerType, handler);
        }

        [Fact]
        public void GetHandler_ForNonExistantCommand_ReturnsNull()
        {
            //Arrange
            const string command = "James >> Push It Up!";

            //Act 
            var handler = new CommandHandlerFactory
            (
                MappingTestData.Empty.Data //TODO: Use Main Mapping test data with correct RegEx
            ).GetHandler(command);

            //Assert
            Assert.Null(handler);
        }

        public static IEnumerable<object[]> Command_Handler_Data()
        {
            yield return new object[] { "Alice -> I love the weather today", typeof(PostingHandler) };
            yield return new object[] { "Charlie follows Bob", typeof(FollowingHandler) };
            yield return new object[] { "Alice", typeof(ReadingHandler) };
            yield return new object[] { "Charlie wall", typeof(WallHandler) };
            yield return new object[] { "£exit£", typeof(ExitHandler) };
        }
    }
}