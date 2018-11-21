using SocialMessenger;
using SocialMessenger.CommandHandlers;
using SocialMessenger.Interfaces;
using System.Collections.Generic;
using System.Linq;
using TestUtilities.TestData;
using Xunit;

namespace IntegrationTests
{
    public class WallHandlerShould
    {
        [Fact]
        public void ProcessCommand_ForExistingUserCharlie_ReturnsCharliesWall()
        {
            new WallHandler
            (
                new UserRepository
                (
                    new Dictionary<string, IUser>()
                    {
                        {UserTestData.User_Data().ElementAt(0).Name,UserTestData.User_Data().ElementAt(0)},
                        {UserTestData.User_Data().ElementAt(1).Name,UserTestData.User_Data().ElementAt(1)},
                    }
                )
            ).ProcessCommand("Charlie wall");
        }
    }
}