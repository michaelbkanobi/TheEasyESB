using MassTransit;
using MassTransit.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace TheEasyEsb.Clerk
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

                sbc.ReceiveEndpoint("test_queue", ep =>
                {
                    ep.Handler<Message>(context =>
                    {
                        return Console.Out.WriteLineAsync($"Received: {context.Message.Text}");
                    });
                });
            });

            await bus.StartAsync(); // This is important!


            Console.WriteLine("Press any key to exit");
            await Task.Run(() => Console.ReadKey());

            await bus.StopAsync();
        }
    }
}
