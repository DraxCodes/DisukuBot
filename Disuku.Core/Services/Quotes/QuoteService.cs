using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disuku.Core.Discord;
using Disuku.Core.Entities;
using Disuku.Core.Entities.Embeds;
using Disuku.Core.Storage;

namespace Disuku.Core.Services.Quotes
{
    public class QuoteService : IQuoteService
    {
        private readonly IDataStore _dataStore;
        private readonly IDiscordMessage _discordMessage;
        private const string TableName = "Quotes";

        public QuoteService(IDataStore dataStore, IDiscordMessage discordMessage)
        {
            _dataStore = dataStore;
            _discordMessage = discordMessage;
            _dataStore.InitializeDbAsync("DisukuBot");
        }

        public async Task Add(ulong chanId, Quote quote)
        {
            await _dataStore.Insert(quote, TableName);
            await _discordMessage.SendDiscordMessageAsync(chanId, "Quote should be added.");
        }

        public async Task Find(ulong chanId, ulong quoteId)
        {
            var quotes = await _dataStore.LoadRecordsAsync<Quote>(x => x.MessageId == quoteId, TableName);
            var selectedQuote = quotes.FirstOrDefault();
            var quoteUrl = $"https://discordapp.com/channels/{selectedQuote.ServerId}/{selectedQuote.ChanId}/{selectedQuote.MessageId}";

            if (!IsCodeblock(selectedQuote.Message))
            {
                var embed = new DisukuEmbed
                {
                    Title = $"{selectedQuote.Author} : Id <{selectedQuote.MessageId}.",
                    Description = $"\n**Quote:** {selectedQuote.Message}\n\n" +
                              $"Jump: [Click Here]({quoteUrl})",
                    Thumbnail = selectedQuote.ThumbnailUrl
                };

                await _discordMessage.SendDiscordEmbedAsync(chanId, embed);
            }
        }

        public async Task Find(ulong chanId, string quoteName)
        {
            var quotes = await _dataStore.LoadRecordsAsync<Quote>(x => x.Name == quoteName, TableName);
            var selectedQuote = quotes.FirstOrDefault();
            var quoteUrl = $"https://discordapp.com/channels/{selectedQuote.ServerId}/{selectedQuote.ChanId}/{selectedQuote.MessageId}";

            if (!IsCodeblock(selectedQuote.Message))
            {
                var embed = new DisukuEmbed
                {
                    Title = $"{selectedQuote.Author} : Id <{selectedQuote.MessageId}.",
                    Description = $"\n**Quote:** {selectedQuote.Message}\n\n" +
                              $"Jump: [Click Here]({quoteUrl})",
                    Thumbnail = selectedQuote.ThumbnailUrl
                };

                await _discordMessage.SendDiscordEmbedAsync(chanId, embed);
            }
            else
            {
                var sb = new StringBuilder();
                sb.Append($"{selectedQuote.Author} : Id: <{selectedQuote.MessageId}>");
                sb.Append($"{selectedQuote.Message}");
                await _discordMessage.SendDiscordMessageAsync(chanId, $"{sb}");
            }

        }

        public async Task List(ulong chanId, DisukuUser user)
        {
            var sb = new StringBuilder();
            var quotes = await _dataStore.LoadRecordsAsync<Quote>(x => x.AuthorId == user.UserId, TableName);
            foreach (var quote in quotes)
            {
                sb.Append($"Author: {quote.Author}\n" +
                          $"Quote: {quote.Name}\n" +
                          $"Id: {quote.MessageId}\n\n");
            }
            await _discordMessage.SendDiscordMessageAsync(chanId, $"{sb}");
        }

        private bool IsCodeblock(string message)
        {
            if (message.StartsWith("```") && message.EndsWith("```"))
            {
                return true;
            }

            return false;
        }
    }
}
