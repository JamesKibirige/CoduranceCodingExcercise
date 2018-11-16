using SocialMessenger.Data;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class UserShould
    {
        [Fact]
        public void PublishMessage_ToPersonalTimeLine_MessageAdded()
        {
            //Arrange
            var user = new User("James", new List<string>());

            //Act
            user.PublishMessage("Hello!");

            //Assert
            Assert.NotEmpty(user.TimeLine);
        }
    }
}
