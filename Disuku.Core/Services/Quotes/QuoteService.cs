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

            if (quotes is null || !quotes.Any())
            {
                await _discordMessage.SendDiscordMessageAsync(chanId, "Quote with that ID was not found.");
                return;
            }
            
            var selectedQuote = quotes.FirstOrDefault();
            var quoteUrl = $"https://discordapp.com/channels/{selectedQuote?.ServerId}/{selectedQuote?.ChanId}/{selectedQuote?.MessageId}";

            if (!IsCodeblock(selectedQuote?.Message))
            {
                var embed = new DisukuEmbed
                {
                    Description = $"\n**Quote:** {selectedQuote?.Message}\n\n" +
                              $"Jump: [Click Here]({quoteUrl})",
                    Author = new Author(selectedQuote?.AuthorUsername, selectedQuote?.AuthorAvatarUrl, $"Id {selectedQuote?.MessageId}")
                };

                await _discordMessage.SendDiscordEmbedAsync(chanId, embed);
            }
            else
            {
                var sb = new StringBuilder();
                sb.Append($"{selectedQuote.AuthorUsername} : Id: <{selectedQuote.MessageId}>");
                sb.Append($"{selectedQuote.Message}");
                await _discordMessage.SendDiscordMessageAsync(chanId, $"{sb}");
            }
        }

        public async Task Find(ulong chanId, string quoteName)
        {
            var quotes = await _dataStore.LoadRecordsAsync<Quote>(x => x.Name == quoteName, TableName);
            if (!quotes.Any()) { await _discordMessage.SendDiscordMessageAsync(chanId, "Quote with that Name was not found."); return; }

            var selectedQuote = quotes.FirstOrDefault();
            var quoteUrl = $"https://discordapp.com/channels/{selectedQuote.ServerId}/{selectedQuote.ChanId}/{selectedQuote.MessageId}";

            if (!IsCodeblock(selectedQuote.Message))
            {
                var embed = new DisukuEmbed
                {
                    Description = $"\n**Quote:** {selectedQuote.Message}\n\n" +
                              $"Jump: [Click Here]({quoteUrl})",
                    Author = new Author(selectedQuote.AuthorUsername, selectedQuote.AuthorAvatarUrl, $"Id {selectedQuote.MessageId}")
                };

                await _discordMessage.SendDiscordEmbedAsync(chanId, embed);
            }
            else
            {
                var sb = new StringBuilder();
                sb.Append($"{selectedQuote.AuthorUsername} : Id: <{selectedQuote.MessageId}>");
                sb.Append($"{selectedQuote.Message}");
                await _discordMessage.SendDiscordMessageAsync(chanId, $"{sb}");
            }

        }

        public async Task List(ulong chanId, DisukuUser user)
        {
            var sb = new StringBuilder();
            var quotes = await _dataStore.LoadRecordsAsync<Quote>(x => x.AuthorId == user.UserId, TableName);

            if (quotes is null || !quotes.Any())
            {
                await _discordMessage.SendDiscordMessageAsync(chanId, "No results found.");
                return;
            }

            foreach (var quote in quotes)
            {
                sb.Append($"AuthorUsername: {quote.AuthorUsername}\n" +
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
