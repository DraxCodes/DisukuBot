using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disuku.Core.Entities;
using Disuku.Core.Storage;

namespace Disuku.Core.Providers.Quotes
{
    public class QuoteProvider : IQuoteProvider
    {
        private readonly IDataStore _dataStore;
        private const string TableName = "Quotes";
        public QuoteProvider(IDataStore dataStore)
        {
            _dataStore = dataStore;
            _dataStore.InitializeDbAsync("DisukuBot");
        }


        public async Task<IEnumerable<Quote>> GetQuotes(ulong userId)
        {
            return await _dataStore.LoadRecordsAsync<Quote>(x => x.AuthorId == userId, TableName);
        }

        public async Task<Quote> GetQuote(ulong messageId)
        {
            var quotes = await _dataStore.LoadRecordsAsync<Quote>(x => x.MessageId == messageId, TableName);
            return quotes.FirstOrDefault();
        }
    }
}
