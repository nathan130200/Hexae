using System;
using DSharpPlus;
using DSharpPlus.Entities;
using Hexae.Entities;

namespace Hexae
{
    public static class HX
    {
        public static class Emoji
        {
            public static readonly string ZeroWidthSpace = "\u200b";
            public static readonly EmojiMetaData TabWidthSpace = (690301952281673925, "<:TabWidthSpace:690301952281673925>");
        }

        public static class Color
        {
            public static readonly DiscordColor HexaeMain = new DiscordColor("#6963E2");
        }
    }
}

namespace Hexae.Entities
{
    public class EmojiMetaData
    {
        public EmojiMetaData(ulong id, string name, string raw)
        {
            this.Id = id;
            this.Name = name;
            this.Raw = raw;
        }

        public ulong Id { get; }
        public string Name { get; }
        public string Raw { get; }

        public DiscordEmoji ToEmoji(DiscordClient client)
        {
            return this.Id == 0 ? DiscordEmoji.FromName(client, this.Name)
                : DiscordEmoji.FromGuildEmote(client, this.Id);
        }

        public static implicit operator EmojiMetaData(ValueTuple<ulong, string> vt) =>
            new EmojiMetaData(vt.Item1, string.Empty, vt.Item2);

        public static implicit operator string(EmojiMetaData mtd) =>
            mtd.ToString();

        public override string ToString()
        {
            return this.Raw;
        }
    }
}