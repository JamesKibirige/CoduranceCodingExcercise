using FluentAssertions;
using System.Linq;
using TestUtilities.TestData;
using Xunit;

namespace UnitTests
{
    public class MappingOptionsTestDataShould
    {
        [Fact]
        public void Main_ReturnAllConfiguredMappings()
        {
            MappingOptionsTestData.Main.Data.Select(d => d.CommandHandler)
                .Should()
                .Contain
                (
                    new[]
                    {
                        "SocialMessenger.CommandHandlers.FollowingHandler",
                        "SocialMessenger.CommandHandlers.PostingHandler",
                        "SocialMessenger.CommandHandlers.ReadingHandler",
                        "SocialMessenger.CommandHandlers.WallHandler"
                    }
                );

        }
    }
}