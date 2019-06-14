using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Disuku.Core.Discord;
using Disuku.Core.Entities;
using Disuku.Core.Storage;

namespace Disuku.Core.Services.Quotes
{
    public class QuoteService : IQuoteService
    {
        private readonly IDbStorage _dbStorage;
        private readonly IDiscordMessage _discordMessage;
        private const string TableName = "Quotes";

        public QuoteService(IDbStorage dbStorage, IDiscordMessage discordMessage)
        {
            _dbStorage = dbStorage;
            _discordMessage = discordMessage;
            _dbStorage.InitializeDbAsync("DisukuBot");
        }

        public async Task Add(ulong chanId, Quote quote)
        {
            await _dbStorage.Insert(quote, TableName);
            await _discordMessage.SendDiscordMessageAsync(chanId, "Quote should be added.");
        }

        public Task Find(ulong chanId, ulong quoteId)
        {
            throw new NotImplementedException();
        }

        public Task Find(ulong chanId, string quoteName)
        {
            throw new NotImplementedException();
        }

        public Task List(ulong chanId, DisukuUser user)
        {
            throw new NotImplementedException();
        }
    }
}
