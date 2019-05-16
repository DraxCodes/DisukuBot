using DisukuBot.DisukuCore.Entities.TMDB;
using System.Threading.Tasks;

namespace DisukuBot.DisukuCore.Services.TMDB
{
    public interface ITmdbService
    {
        Task<Movie> GetMovieAsync(string name);
        Task<MovieCollection> GetMovieCollectionAsync(string collectionName);
        Task<TVShow> GetTvShowAsync(string name);
    }
}
