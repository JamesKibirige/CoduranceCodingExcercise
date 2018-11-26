using FluentAssertions;
using Microsoft.Extensions.Configuration;
using SocialMessenger.Configurations;
using SocialMessenger.Options;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IntegrationTests
{
    public class ConfigurationShould
    {
        [Fact]
        public void CreateDevelopmentConfiguration_WithoutThrowingException_ReturnsCommandHandlerMappingConfiguration()
        {
            var configuration = Configuration.Development.ConfigurationRoot;

            Assert.NotNull(configuration);
            configuration
                .GetSection(Resources.CommandHandlerMappingsConfigurationKey)
                .Get<IEnumerable<CommandHandlerMappingOptions>>()
                .Select(m => m.CommandHandler)
                .Should().Contain
                (
                    new[]
                    {
                        "SocialMessenger.CommandHandlers.FollowingHandler",
                        "SocialMessenger.CommandHandlers.PostingHandler",
                        "SocialMessenger.CommandHandlers.ReadingHandler",
                        "SocialMessenger.CommandHandlers.WallHandler",
                        "SocialMessenger.CommandHandlers.ExitHandler"
                    }
                );
        }
    }
}