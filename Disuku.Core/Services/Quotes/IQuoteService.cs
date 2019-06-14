using System.Threading.Tasks;
using Disuku.Core.Entities;

namespace Disuku.Core.Services.Quotes
{
    public interface IQuoteService
    {
        Task Find(ulong chanId, ulong quoteId);
        Task Find(ulong chanId, string quoteName);
        Task Add(ulong chanId, Quote quote);
        Task List(ulong chanId, DisukuUser user);
    }
}
