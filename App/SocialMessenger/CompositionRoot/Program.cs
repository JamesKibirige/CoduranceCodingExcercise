using SocialMessenger.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SocialMessenger.CompositionRoot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var application = new Application
                (
                    new CommandHandlerFactory
                    (
                        new Dictionary<Regex, ICommandHandler>()
                    )
                );

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
