using FluentAssertions;
using SocialMessenger;
using SocialMessenger.Interfaces;
using System;
using System.Collections.Generic;
using TestUtilities.MockBuilders;
using Xunit;

namespace UnitTests
{
    public class MockUserRepositoryBuilderShould
    {
        [Theory]
        [MemberData(nameof(HasUser_Data))]
        public void WithHasUser_SetUpHasUserMethod_VerifyCollaborators(bool hasUser)
        {
            var result = new MockUserRepositoryBuilder()
                .WithHasUser(hasUser)
                .Build();

            Assert.Equal
            (
                hasUser,
                result.HasUser("AnyUser")
            );
        }

        [Theory]
        [MemberData(nameof(User_Data))]
        public void WithGetUser_SetUpGetUserMethod_VerifyCollaborators(IUser user)
        {
            var result = new MockUserRepositoryBuilder()
                .WithGetUser(user)
                .Build();

            Assert.Equal
            (
                user,
                result.GetUser(user.Name)
            );
        }

        [Theory]
        [MemberData(nameof(User_Data))]
        public void WithAddUser_SetUpAddUserMethod_VerifyCollaborators(IUser user)
        {
            var result = new MockUserRepositoryBuilder()
                .WithAddUser()
                .Build();

            Action addUser = () => result.AddUser(user);
            addUser.Should().NotThrow<Exception>();
        }


        [Fact]
        public void Build_GenerateMockUserRepository()
        {
            var result = new MockUserRepositoryBuilder()
                .Build();

            Assert.NotNull(result);
        }

        public static IEnumerable<object[]> HasUser_Data()
        {
            yield return new object[] { true };
            yield return new object[] { false };
        }

        public static IEnumerable<object[]> User_Data()
        {
            yield return new object[]
            {
                new User
                (
                    "James",
                    new TimeLine(new Dictionary<DateTimeOffset, string>()),
                    new Dictionary<string, ITimeLine>()
                )
            };
            yield return new object[]
            {
                new User
                (
                    "Sophie",
                    new TimeLine(new Dictionary<DateTimeOffset, string>()),
                    new Dictionary<string, ITimeLine>()
                )
            };
            yield return new object[]
            {
                new User
                (
                    "Anna",
                    new TimeLine(new Dictionary<DateTimeOffset, string>()),
                    new Dictionary<string, ITimeLine>()
                )
            };
        }

        public static IEnumerable<object[]> Users_Data()
        {
            yield return new object[]
            {
                new List<User>()
                {
                    new User
                    (
                        "James",
                        new TimeLine(new Dictionary<DateTimeOffset, string>()),
                        new Dictionary<string, ITimeLine>()
                    ),
                    new User
                    (
                        "Kato",
                        new TimeLine(new Dictionary<DateTimeOffset, string>()),
                        new Dictionary<string, ITimeLine>()
                    ),
                    new User
                    (
                        "Sophie",
                        new TimeLine(new Dictionary<DateTimeOffset, string>()),
                        new Dictionary<string, ITimeLine>()
                    ),
                    new User
                    (
                        "Anna",
                        new TimeLine(new Dictionary<DateTimeOffset, string>()),
                        new Dictionary<string, ITimeLine>()
                    )
                }
            };
        }
    }
}
