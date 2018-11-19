using Microsoft.Extensions.Configuration;
using SocialMessenger.Configurations;
using SocialMessenger.Enumerations;
using SocialMessenger.Factories;
using SocialMessenger.Interfaces;
using SocialMessenger.Options;
using System;
using System.Collections.Generic;

namespace SocialMessenger.CompositionRoot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                //Create Application instance
                var application = new Application
                (
                    new CommandHandlerFactory
                    (
                        new CommandHandlerMappingFactory
                            (
                                Configuration
                                    .Development
                                    .ConfigurationRoot
                                    .GetSection(ConfigurationKey.CommandHandlerMappings)
                                    .Get<IEnumerable<CommandHandlerMappingOptions>>(),
                                new UserRepository
                                (
                                    new Dictionary<string, IUser>()
                                )
                            )
                            .CreateMappings()
                    )
                );

                //Run application
                application.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine("An Error has occurred!");
                Console.WriteLine(e.Message);
            }
        }
    }
}
