using System.Collections.Generic;
using System.Threading.Tasks;
using Disuku.Core.Entities;

namespace Disuku.Core.Providers.Quotes
{
    public interface IQuoteProvider
    {
        Task<IEnumerable<Quote>> GetQuotes(ulong userId);
        Task<Quote> GetQuote(ulong messageId);
    }
}
