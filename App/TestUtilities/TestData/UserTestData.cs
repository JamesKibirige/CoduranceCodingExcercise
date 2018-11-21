using SocialMessenger;
using SocialMessenger.Data;
using SocialMessenger.Interfaces;
using System;
using System.Collections.Generic;

namespace TestUtilities.TestData
{
    public class UserTestData
    {
        public static IEnumerable<User> User_Data()
        {
            yield return new User
            (
                "Charlie",
                new TimeLine
                (
                    new Dictionary<DateTimeOffset, string>()
                ),
                new Dictionary<string, ITimeLine>()
                {
                    {
                        "Sophie",
                        new TimeLine
                        (
                            new Dictionary<DateTimeOffset, string>()
                            {
                                {
                                    new DateTimeOffset(2018, 11, 16, 0, 0, 0, TimeSpan.Zero),
                                    "Hi my name is Sophie!"
                                },
                                {
                                    new DateTimeOffset(2018, 11, 16, 0, 5, 0, TimeSpan.Zero),
                                    "I live in London!"
                                },
                                {
                                    new DateTimeOffset(2018, 11, 16, 0, 10, 0, TimeSpan.Zero),
                                    "I am 27 years old and female..."
                                },
                                {
                                    new DateTimeOffset(2018, 11, 16, 0, 15, 0, TimeSpan.Zero),
                                    "I love art and craft!"
                                }
                            }
                        )
                    }
                }
            );

            yield return new User
            (
                "Sophie",
                new TimeLine
                (
                    new Dictionary<DateTimeOffset, string>()
                ),
                new Dictionary<string, ITimeLine>()
            );
        }
    }
}