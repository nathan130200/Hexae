using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Hexae.Extensions;

namespace Hexae.Commands
{
    public class InformationCommands : BaseCommandModule
    {
        [Command]
        public async Task Ping(CommandContext ctx)
        {
            var watch = Stopwatch.StartNew();
            await ctx.TriggerTypingAsync();
            watch.Stop();

            var stbl = new StringBuilder()
                .AppendLine(":white_small_square: Rest")
                .AppendLine($"{HX.Emoji.TabWidthSpace} - {watch.ElapsedMilliseconds}ms")
                .AppendLine("\u200b")
                .AppendLine(":white_small_square: Gateway")
                .AppendLine($"{HX.Emoji.TabWidthSpace} - {ctx.Client.Ping}ms");

            await ctx.RespondAsync(embed: ctx.CreateEmbed(x =>
            {
                x.Description(stbl.ToString());
                x.Color(HX.Color.HexaeMain);
                x.RequestedBy(ctx.User);
                x.Timestamp(DateTime.Now);
            }));
        }
    }
}
