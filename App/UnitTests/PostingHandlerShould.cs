﻿using Moq;
using SocialMessenger;
using SocialMessenger.CommandHandlers;
using SocialMessenger.Interfaces;
using System;
using System.Collections.Generic;
using TestUtilities.MockBuilders;
using Xunit;
namespace UnitTests
{
    public class PostingHandlerShould
    {
        [Fact]
        public void ProcessCommand_ExistingUser_VerifyCollaborators()
        {
            //Arrange
            var user = Mock.Of<IUser>();
            Mock.Get(user)
                .Setup(m => m.PublishMessage(It.IsAny<DateTimeOffset>(), It.IsAny<string>()));

            var userRepository = new MockUserRepositoryBuilder()
                .WithHasUser(true)
                .WithGetUser(user)
                .Build();

            //Act
            new PostingHandler(userRepository).ProcessCommand("James -> Hello, Hello");

            //Assert
            Mock.Get(userRepository)
                .Verify(m => m.HasUser(It.IsAny<string>()));
            Mock.Get(userRepository)
                .Verify(m => m.GetUser(It.IsAny<string>()));
            Mock.Get(user)
                .Verify(m => m.PublishMessage(It.IsAny<DateTimeOffset>(), It.IsAny<string>()));
        }

        [Fact]
        public void ProcessCommand_NonExistingUser_VerifyCollaborators()
        {
            //Arrange
            var userRepository = new MockUserRepositoryBuilder()
                .WithHasUser(false)
                .WithAddUser()
                .Build();

            //Act
            new PostingHandler(userRepository)
                .ProcessCommand("James -> Hello, Hello");

            //Assert
            Mock.Get(userRepository)
                .Verify(m => m.HasUser(It.IsAny<string>()));
            Mock.Get(userRepository)
                .Verify(m => m.AddUser(It.IsAny<IUser>()));
        }

        [Fact]
        public void ProcessCommand_NonExistingUser_AddsNewUserToRepository()
        {
            //Arrange
            var userRepository = new UserRepository
            (
                new Dictionary<string, IUser>()
            );

            //Act
            new PostingHandler(userRepository)
                .ProcessCommand("James -> Hello, Hello");

            //Assert
            Assert.True(userRepository.HasUser("James"));
        }
    }
}