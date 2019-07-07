using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Disuku.Core.Entities;
using Disuku.Core.Services.Quotes;
using Disuku.Discord.Converters;

namespace Disuku.Discord.Modules
{
    [Name("Quote Commands")]
    [Group("Quote")]
    public class Quotes : ModuleBase<SocketCommandContext>
    {
        private readonly IQuoteService _quoteService;
        public Quotes(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [Command]
        public async Task QuoteDisplay(ulong quoteId)
        {
            await _quoteService.Find(Context.Channel.Id, quoteId);
        }

        [Command]
        public async Task QuoteDisplay([Remainder] string quoteName)
        {
             await _quoteService.Find(Context.Channel.Id, quoteName);
        }

        [Command("Add")]
        public async Task QuoteAdd(ulong quoteId, [Remainder] string quoteName)
        {
            var message = await Context.Channel.GetMessageAsync(quoteId);
            if (message is null)
            {
                await ReplyAsync("Message not found, are you in the same channel as the quote?");
                return;
            }

            var quote = new Quote
            {
                MessageId = quoteId,
                ChanId = Context.Channel.Id,
                ServerId = Context.Guild.Id,
                Name = quoteName,
                Message = message.Content,
                AuthorUsername = message.Author.Username,
                AuthorId = message.Author.Id,
                AuthorAvatarUrl = message.Author.GetAvatarUrl()
            };

            await _quoteService.Add(Context.Channel.Id, quote);
        }

        [Command("List")]
        public async Task ListQuotes(SocketGuildUser user)
        {
            var disukuUser = DisukuEntityConverter.ConvertToDisukuUser(user);
            await _quoteService.List(Context.Channel.Id, disukuUser);
        }
    }
}
