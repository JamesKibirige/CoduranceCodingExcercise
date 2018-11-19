using System.Linq;
using TestUtilities.TestData;
using Xunit;
namespace TestUtilityUnitTests
{
    public class MappingOptionsTestDataShould
    {
        [Fact]
        public void Main_ReturnAllConfiguredMappings()
        {
            //Arrange
            //Act
            //Assert
            Assert.Equal(4, MappingOptionsTestData.Main.Data.Count());
        }
    }
}