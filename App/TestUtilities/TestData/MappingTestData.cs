using SocialMessenger;
using SocialMessenger.CommandHandlers;
using SocialMessenger.Interfaces;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TestUtilities.TestData
{
    public abstract class MappingTestData
    {
        public static readonly MappingTestData Main = new MainTestData();
        public static readonly MappingTestData Empty = new EmptyTestData();

        public IDictionary<Regex, ICommandHandler> Data { get; set; }

        private MappingTestData(IDictionary<Regex, ICommandHandler> data)
        {
            Data = data;
        }

        private class MainTestData : MappingTestData
        {
            public MainTestData()
                : base
                (
                    new Dictionary<Regex, ICommandHandler>()
                    {
                        {
                            new Regex(@"^[a-zA-Z]+\b follows \b[a-zA-Z]+$"),
                            new FollowingHandler
                            (
                                new UserRepository
                                (
                                    new Dictionary<string, IUser>()
                                )
                            )
                        },
                        {
                            new Regex(@"^[a-zA-Z]+\b -> (.)+$"),
                            new PostingHandler
                            (
                                new UserRepository
                                (
                                    new Dictionary<string, IUser>()
                                )
                            )
                        },
                        {
                            new Regex(@"^[a-zA-Z]+$"),
                            new ReadingHandler
                            (
                                new UserRepository
                                (
                                    new Dictionary<string, IUser>()
                                )
                            )
                        },
                        {
                            new Regex(@"^[a-zA-Z]+\b wall$"),
                            new WallHandler
                            (
                                new UserRepository
                                (
                                    new Dictionary<string, IUser>()
                                )
                            )
                        },
                    }
                )
            {
            }
        }

        private class EmptyTestData : MappingTestData
        {
            public EmptyTestData()
                : base
                (
                    new Dictionary<Regex, ICommandHandler>()
                    {
                    }
                )
            {
            }
        }
    }
}