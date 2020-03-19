using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using Hexae.Entities.Editors;
using System;

namespace Hexae.Extensions
{
    public static class DSharpPlusExtensions
    {
        public static string Format(this DiscordUser user) =>
            $"{user.Username}#{user.Discriminator}";

        public static DiscordEmbedBuilder WithRequestedBy(this DiscordEmbedBuilder builder, DiscordUser user) =>
            builder.WithFooter(user.Format(), user.AvatarUrl);
        
        public static DiscordEmbed CreateEmbed(this CommandContext ctx, Action<EmbedEditor> fn)
        {
            var embed = new DiscordEmbedBuilder();
            embed.Mutate(fn);
            return embed;
        }
    }
}
