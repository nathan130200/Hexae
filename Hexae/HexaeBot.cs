using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using Hexae.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Hexae
{
	public class HexaeBot
	{
		public IServiceProvider Services { get; private set; }
		public HexaeSettings Settings { get; private set; }
		public DiscordClient Discord { get; private set; }
		public CommandsNextExtension CommandsNext { get; private set; }
		public InteractivityExtension Interactivity { get; private set; }

		public HexaeBot(HexaeSettings settings)
		{
			Singleton<HexaeBot>.Instance = this;

			this.Settings = settings;
			this.Discord = new DiscordClient(this.Settings.Discord.Build());
			this.Interactivity = this.Discord.UseInteractivity(this.Settings.Interactivity.Build());

			this.Services = new ServiceCollection()
				.BuildServiceProvider(true);

			this.CommandsNext = this.Discord.UseCommandsNext(new CommandsNextConfiguration
			{
				StringPrefixes = new[] { "hexae!", "h!", "." },
				EnableDms = false,
				DmHelp = true,
				Services = this.Services
			});

			this.CommandsNext.RegisterCommands(typeof(HexaeBot).Assembly);
		}

		public Task InitializeAsync() =>
			this.Discord.ConnectAsync();

		public Task ShutdownAsync() =>
			this.Discord.DisconnectAsync();
	}
}
