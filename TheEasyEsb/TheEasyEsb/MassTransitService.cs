using MassTransit;
using MassTransit.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace TheEasyEsb
{
    public class MassTransitService
    {
        private readonly ILoggerFactory factory;
        public MassTransitService(ILoggerFactory factory)
        {
            this.factory = factory;
        }

        public async Task Run()
        {
            LogContext.ConfigureCurrentLogContext(factory);
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host("rabbitmq://localhost");
            });

            await bus.StartAsync();

            for (int i = 0; i < 10; i++)
            {
                var id = Guid.NewGuid();
                await bus.Publish(new Message { MessageId = id, Text = $"The is Message {id}" });
            }

            Console.WriteLine("Press any key to exit");
            await Task.Run(() => Console.ReadKey());

            await bus.StopAsync();
        }
    }
}
