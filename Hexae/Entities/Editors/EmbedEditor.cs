using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using Hexae.Entities.Editors;
using Hexae.Extensions;

public static class EmbedEditorExtensions
{
    public static DiscordEmbedBuilder Mutate(this DiscordEmbedBuilder embed, Action<EmbedEditor> editor)
    {
        editor?.Invoke(new EmbedEditor(embed));
        return embed;
    }

    public static DiscordEmbed Mutate(this DiscordEmbed embed, Action<EmbedEditor> editor)
    {
        editor?.Invoke(new EmbedEditor(embed));
        return embed;
    }

    public static EmbedEditor RequestedBy(this EmbedEditor editor, DiscordUser user)
    {
        editor.Footer($"Solicitado por {user.Format()}", user.AvatarUrl);
        return editor;
    }
}

namespace Hexae.Entities.Editors
{
    public class EmbedEditor
    {
        public DiscordEmbedBuilder Embed { get; }

        public EmbedEditor(DiscordEmbedBuilder builder)
        {
            this.Embed = builder;
        }

        public EmbedEditor(DiscordEmbed other = default)
        {
            this.Embed = other is null ?
                new DiscordEmbedBuilder() : new DiscordEmbedBuilder(other);
        }

        public EmbedEditor Field(object name, object value) => this.Field(name, value, true);
        public EmbedEditor Url(Uri url = default) => this.Url(url.ToString());
        public EmbedEditor Image(Uri url = default) => this.Image(url.ToString());
        public EmbedEditor Thumbnail(Uri url = default) => this.Thumbnail(url.ToString());
        public EmbedEditor Author(string name = default, Uri url = default, Uri icon_url = default) => this.Author(name, url?.ToString(), icon_url?.ToString());
        public EmbedEditor Footer(string text = default, Uri icon_url = default) => this.Footer(text, icon_url?.ToString());

        public EmbedEditor Field(object name, object value, bool inline)
        {
            if (this.Embed.Fields.Count == 25)
                return this; // avoid exceptions.

            if (name is null)
                return this;

            var str_name = $"{HX.Emoji.ZeroWidthSpace}{name}";
            var str_value = $"{HX.Emoji.ZeroWidthSpace}{value}";

            this.TruncateString(ref str_name, 256);
            this.TruncateString(ref str_value, 1024);

            this.Embed.AddField(str_name, str_value, inline);
            return this;
        }

        public EmbedEditor Title(string value = default)
        {
            this.Embed.WithTitle(value);
            return this;
        }

        public EmbedEditor Description(string value = default)
        {
            this.TruncateString(ref value, 2000);
            this.Embed.WithDescription(value);
            return this;
        }

        protected void TruncateString(ref string value, int max_length)
        {
            if (string.IsNullOrEmpty(value))
                return;

            var sfx = "[...]";

            if (value.Length > max_length)
                value = value.Substring(0, max_length - sfx.Length) + sfx;
        }

        public EmbedEditor Color(DiscordColor? color = default)
        {
            this.Embed.Color = color.HasValue ? color.Value
                : Optional.FromNoValue<DiscordColor>();

            return this;
        }

        public EmbedEditor Image(string url = default)
        {
            this.Embed.WithImageUrl(url);
            return this;
        }

        public EmbedEditor Url(string url = default)
        {
            this.Embed.WithUrl(url);
            return this;
        }

        public EmbedEditor Thumbnail(string url = default)
        {
            this.Embed.WithThumbnailUrl(url);
            return this;
        }

        public EmbedEditor Color(string hex)
        {
            this.Embed.Color = new DiscordColor(hex);
            return this;
        }

        public EmbedEditor Color(int value)
        {
            this.Embed.Color = new DiscordColor(value);
            return this;
        }

        public EmbedEditor Color(byte r, byte g, byte b)
        {
            this.Embed.Color = new DiscordColor(r, g, b);
            return this;
        }

        public EmbedEditor Color(float r, float g, float b)
        {
            this.Embed.Color = new DiscordColor(r, g, b);
            return this;
        }

        public EmbedEditor Author(string name = default, string url = default, string icon_url = default)
        {
            this.Embed.WithAuthor(name, url, icon_url);
            return this;
        }

        public EmbedEditor Footer(string text = default, string icon_url = default)
        {
            this.TruncateString(ref text, 2048);
            this.Embed.WithFooter(text, icon_url);
            return this;
        }

        public EmbedEditor Timestamp()
        {
            this.Embed.WithTimestamp(DateTime.UtcNow);
            return this;
        }

        public EmbedEditor Timestamp(DateTimeOffset? dto = default)
        {
            this.Embed.WithTimestamp(dto);
            return this;
        }

        public EmbedEditor Timestamp(DateTime? dt = default)
        {
            this.Embed.WithTimestamp(dt);
            return this;
        }

        public EmbedEditor Timestamp(ulong snowflake)
        {
            this.Embed.WithTimestamp(snowflake);
            return this;
        }
    }
}
