using System;
using System.Threading;
using System.Threading.Tasks;
using Hexae.Entities;

namespace Hexae
{
    static class Program
    {
        static readonly CancellationTokenSource Cts
          = new CancellationTokenSource();

        static async Task Main(string[] args)
        {
            var settings = await HexaeSettings.InitializeAsync();

            if (!settings.Discord.IsValid)
                return;

            Console.CancelKeyPress += (sender, e) =>
            {
                if (!Cts.IsCancellationRequested)
                    Cts.Cancel();

                e.Cancel = true;
            };

            var bot = new HexaeBot(settings);
            await bot.InitializeAsync();

            while (!Cts.IsCancellationRequested)
                await Task.Delay(1);

            await bot.ShutdownAsync();
        }
    }
}
