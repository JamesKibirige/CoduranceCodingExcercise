using SocialMessenger.Configurations;
using Xunit;

namespace IntegrationTests
{
    public class ConfigurationShould
    {
        [Fact]
        public void CreateDevelopmentConfiguration_WithoutThrowingException()
        {
            //Arrange
            //Act
            var configuration = Configuration.Development.ConfigurationRoot;

            //Assert
            Assert.NotNull(configuration);
        }
    }
}