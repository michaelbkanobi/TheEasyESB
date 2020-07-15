using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TheEasyEsb
{
    class Program
    {
        public static async Task Main()
        {
            var container = new ServiceCollection()
                .AddLogging(c => c.SetMinimumLevel(LogLevel.Information).AddConsole())
                .AddSingleton<MassTransitService>()
                .BuildServiceProvider();

            var logger = container.GetRequiredService<ILogger<Program>>();
            var service = container.GetRequiredService<MassTransitService>();
            logger.LogInformation("Starting application");
            await service.Run();
            logger.LogInformation("Stopping application");
        }
    }
}
