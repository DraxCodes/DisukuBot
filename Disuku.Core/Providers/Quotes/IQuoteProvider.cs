using System.Collections.Generic;
using Disuku.Core.Entities;

namespace Disuku.Core.Providers.Quotes
{
    public interface IQuoteProvider
    {
        IEnumerable<Quote> GetQuotes(ulong messageId);
        Quote GetQuote(ulong userId);
    }
}
