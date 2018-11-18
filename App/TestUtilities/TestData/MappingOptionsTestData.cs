﻿using SocialMessenger.Options;
using System.Collections.Generic;

namespace TestUtilities.TestData
{
    public abstract class MappingOptionsTestData
    {
        public static readonly MappingOptionsTestData Main = new MainTestData();
        public static readonly MappingOptionsTestData Empty = new EmptyRegExTestData();

        public IEnumerable<CommandHandlerMappingOptions> Data { get; set; }

        private MappingOptionsTestData(IEnumerable<CommandHandlerMappingOptions> data)
        {
            Data = data;
        }

        private class MainTestData : MappingOptionsTestData
        {
            public MainTestData()
                : base
                (
                    new List<CommandHandlerMappingOptions>()
                    {
                        new CommandHandlerMappingOptions("[az]", "SocialMessenger.CommandHandlers.FollowingHandler"),
                        new CommandHandlerMappingOptions("[az]", "SocialMessenger.CommandHandlers.PostingHandler"),
                        new CommandHandlerMappingOptions("[az]", "SocialMessenger.CommandHandlers.ReadingHandler"),
                        new CommandHandlerMappingOptions("[az]", "SocialMessenger.CommandHandlers.WallHandler")
                    }
                )
            {
            }
        }

        private class EmptyRegExTestData : MappingOptionsTestData
        {
            public EmptyRegExTestData()
                : base
                (
                    new List<CommandHandlerMappingOptions>()
                    {
                        new CommandHandlerMappingOptions("", "SocialMessenger.CommandHandlers.FollowingHandler"),
                        new CommandHandlerMappingOptions("", "SocialMessenger.CommandHandlers.PostingHandler"),
                        new CommandHandlerMappingOptions("", "SocialMessenger.CommandHandlers.ReadingHandler"),
                        new CommandHandlerMappingOptions("", "SocialMessenger.CommandHandlers.WallHandler")
                    }
                )
            {
            }
        }
    }
}