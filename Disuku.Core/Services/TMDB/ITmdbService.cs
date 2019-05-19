using Disuku.Core.Entities.TMDB;
using System.Threading.Tasks;

namespace Disuku.Core.Services.TMDB
{
    public interface ITmdbService
    {
        Task ReplyMovieAsync(ulong chanId, string name);
        Task<MovieCollection> GetMovieCollectionAsync(string collectionName);
        Task<TVShow> GetTvShowAsync(string name);
    }
}
