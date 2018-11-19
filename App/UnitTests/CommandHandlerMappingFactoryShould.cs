using FluentAssertions;
using Moq;
using SocialMessenger;
using SocialMessenger.Interfaces;
using System.Linq;
using TestUtilities.TestData;
using Xunit;

namespace UnitTests
{
    public class CommandHandlerMappingFactoryShould
    {
        [Fact]
        public void GenerateMappings_ForConfiguredCommandHandlers_ReturnsMappings()
        {
            //Arrange
            var userRepository = Mock.Of<IUserRepository>();

            //Act
            var mappings = new CommandHandlerMappingFactory
            (
                MappingOptionsTestData.Main.Data,
                userRepository

            ).CreateMappings();

            //Assert
            mappings.Should().NotBeEmpty();
            mappings.Should().HaveCount(MappingOptionsTestData.Main.Data.Count());
            mappings.Values.Select(n => n.GetType().FullName)
                .Should()
                .BeEquivalentTo
                (
                    MappingOptionsTestData.Main.Data.Select(n => n.CommandHandler)
                );
        }

        [Fact]
        public void GenerateMappings_ForEmptyRegExCommandHandlers_ReturnsNoMappings()
        {
            //Arrange
            var userRepository = Mock.Of<IUserRepository>();

            //Act
            var mappings = new CommandHandlerMappingFactory
            (
                MappingOptionsTestData.Empty.Data,
                userRepository

            ).CreateMappings();

            //Assert
            mappings.Should().BeEmpty();
        }
    }
}