using System.Threading.Tasks;

namespace Disuku.Core.Services.TMDB
{
    public interface ITmdbService
    {
        Task ReplyMovieAsync(ulong chanId, string name);
        Task ReplyMovieCollectionAsync(ulong chanId, string collectionName);
        Task ReplyTvShowAsync(ulong chanId, string name);
    }
}
