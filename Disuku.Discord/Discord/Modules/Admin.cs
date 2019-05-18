using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Disuku.Discord.Modules
{
    public class Admin : ModuleBase<SocketCommandContext>
    {
        [Command("Ban", RunMode = RunMode.Async), Name("Ban Member"), RequireUserPermission(GuildPermission.BanMembers)]
        [Summary("Bans a users")]
        public async Task BaneMember(SocketGuildUser user)
        {
            await user.BanAsync();
            var banMsg = await ReplyAsync($"{user.Username} has been banned.");
            await Task.Delay(5000);
            await banMsg.DeleteAsync();
            await Context.Message.DeleteAsync();
        }

        [Command("Kick", RunMode = RunMode.Async), Name("Kick Member"), RequireUserPermission(GuildPermission.KickMembers)]
        [Summary("Kicks a user.")]
        public async Task KickMember(SocketGuildUser user)
        {
            await user.KickAsync();
            var kickMsg = await ReplyAsync($"{user.Username} has been kicked.");
            await Task.Delay(5000);
            await kickMsg.DeleteAsync();
            await Context.Message.DeleteAsync();
        }

        [Group("Purge"), RequireUserPermission(GuildPermission.ManageMessages)]
        public class AdminPurge : ModuleBase<SocketCommandContext>
        {
            [Command(RunMode = RunMode.Async), Name("Purge Chat")] 
            [Summary("Purges messages from the channel")]
            public async Task PurgeChannel(int num)
            {
                if (num < 1)
                {
                    await ReplyAsync("Enter a number greater than 1");
                }
                else
                {
                    var messages = await Context.Channel
                        .GetMessagesAsync(num)
                        .FlattenAsync();

                    var date = DateTime.Now.AddDays(-7);

                    if (messages.Count(x => x.CreatedAt > date) == 1 && num > 1)
                    {
                        var reply = await ReplyAsync("Sorry I can only delete messages that are less than 2 weeks old.");
                        await Task.Delay(5000);
                        await reply.DeleteAsync();
                    }

                    await (Context.Channel as SocketTextChannel)?
                        .DeleteMessagesAsync(messages.Where(x => x.CreatedAt > date));
                }
            }

            [Command("User", RunMode = RunMode.Async), Name("Purge User")]
            [Summary("Purges messages in a channel for a specified user.")]
            public async Task PurgeUser(SocketGuildUser user, int num)
            {
                if (num < 1)
                {
                    await ReplyAsync("Enter a number greater than 1");
                }
                else
                {
                    var downloadedMessages = await Context.Channel
                        .GetMessagesAsync(num)
                        .FlattenAsync();

                    var messages = downloadedMessages
                        .ToList()
                        .Where((m => m.Id == user.Id));

                    var date = DateTime.Now.AddDays(-7);

                    if (messages.Count(x => x.CreatedAt > date) == 1 && num > 1)
                    {
                        var reply = await ReplyAsync("Sorry I can only delete messages that are less than 2 weeks old.");
                        await Task.Delay(5000);
                        await reply.DeleteAsync();
                    }

                    await (Context.Channel as SocketTextChannel)?
                        .DeleteMessagesAsync(messages.Where(x => x.CreatedAt > date));
                }
            }
        }
    }
}
