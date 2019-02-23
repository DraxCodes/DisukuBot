using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using DisukuBot.DisukuCore.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DisukuBot.DisukuDiscord.Modules
{
    public class Admin : ModuleBase<SocketCommandContext>
    {
        [Command("Ban"), Name("Ban Member"), RequireUserPermission(GuildPermission.BanMembers)]
        [Summary("Bans a users")]
        public async Task BaneMember(SocketGuildUser user)
        {
            await user.BanAsync();
            await ReplyAsync($"{user.Username} has been banned.");
        }

        [Command("Kick"), Name("Kick Member"), RequireUserPermission(GuildPermission.KickMembers)]
        [Summary("Kicks a user.")]
        public async Task KickMember(SocketGuildUser user)
        {
            await user.KickAsync();
            await ReplyAsync($"{user.Username} has been kicked.");
        }

        [Group("Purge"), RequireUserPermission(GuildPermission.ManageMessages)]
        public class AdminPurge : ModuleBase<SocketCommandContext>
        {
            [Command, Name("Purge Chat")] 
            [Summary("Purges messages from the channel")]
            public async Task PurgeChannel(int num)
            {
                if (num < 1)
                {
                    await ReplyAsync("Enter a number greater than 1");
                }
                else
                {
                    //TODO: Add check for if messages are greater than 2weeks old.
                    var messages = await Context.Channel.GetMessagesAsync(num).FlattenAsync();
                    await (Context.Channel as SocketTextChannel)
                        .DeleteMessagesAsync(messages);
                }
            }

            [Command("User"), Name("Purge User")]
            [Summary("Purges messages in a channel for a specified user.")]
            public async Task PurgeUser(SocketGuildUser user, int num)
            {
                if (num < 1)
                {
                    await ReplyAsync("Enter a number greater than 1");
                }
                else
                {
                    var messages = await Context.Channel.GetMessagesAsync(num).FlattenAsync();
                    await (Context.Channel as SocketTextChannel)
                        .DeleteMessagesAsync(messages
                        .Where(m => m.Id == user.Id));
                }
            }
        }
    }
}
