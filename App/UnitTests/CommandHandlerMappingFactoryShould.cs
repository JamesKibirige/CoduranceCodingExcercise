using FluentAssertions;
using Moq;
using SocialMessenger.Factories;
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
            var userRepository = Mock.Of<IUserRepository>();

            var mappings = new CommandHandlerMappingFactory
            (
                MappingOptionsTestData.Main.Data,
                userRepository

            ).CreateMappings();

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

            var userRepository = Mock.Of<IUserRepository>();


            var mappings = new CommandHandlerMappingFactory
            (
                MappingOptionsTestData.Empty.Data,
                userRepository

            ).CreateMappings();

            mappings.Should().BeEmpty();
        }
    }
}