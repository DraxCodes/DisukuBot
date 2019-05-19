using Disuku.Core.Entities.TMDB;
using System.Threading.Tasks;

namespace Disuku.Core.Services.TMDB
{
    public interface ITmdbService
    {
        Task ReplyMovieAsync(ulong chanId, string name);
        Task GetMovieCollectionAsync(string collectionName);
        Task GetTvShowAsync(string name);
    }
}
