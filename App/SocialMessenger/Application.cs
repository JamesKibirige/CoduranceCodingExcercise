using CoduranceSocialMessenger.Interfaces;
using SocialMessenger.Interfaces;
using System;

namespace SocialMessenger
{
    public class Application : IRunnable
    {
        private readonly ICommandHandlerFactory _handlerFactory;

        public Application(ICommandHandlerFactory handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }

        public void Run()
        {
            while (true)
            {
                Console.Write(">");
                var inputcommand = Console.ReadLine();

                var handler = _handlerFactory.GetHandler(inputcommand);

                handler?.ProcessCommand(inputcommand);
            }
        }
    }
}