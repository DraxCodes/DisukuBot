using DisukuBot.DisukuCore.Entities.TMDB;
using DisukuData;
using DisukuData.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDbLib.Client;
using System.Linq;
using TMDbLib.Objects.Search;

namespace DisukuBot.DisukuCore.Services.TMDB
{
    public class TmdbService : ITmdbService
    {
        private IDisukuJsonDataService _dataService;
        private TMDbClient _client;
        private TMDBConfig _config;


        //TODO: MAKE COLLECTION ENTITY!!!!!!!!


        public TmdbService(IDisukuJsonDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task InitializeAsync()
        {
            _config = await GetConfig();
            _client = new TMDbClient(_config.Token);
        }

        public async Task<Movie> GetMovieAsync(string name)
        {
            var search = await _client.SearchMovieAsync(name);

            var result = search?.Results.First();

            return new Movie
            {
                Title = result.Title,
                Description = result.Overview,
                ImageUrl = $"http://image.tmdb.org/t/p/w500{result.PosterPath}",
                Url = $"https://www.themoviedb.org/movie/{result.Id}",
                BackdropUrl = $"http://image.tmdb.org/t/p/w500{result.BackdropPath}",
                ReleaseDate = result.ReleaseDate.Value
            };
        }

        public async Task<List<Movie>> GetMovieCollectionAsync(string collectionName)
        {
            var search = await _client.SearchCollectionAsync(collectionName);
            var collection = await _client.GetCollectionAsync(search.Results.First().Id);
            return ConvertToDisukuMovie(collection.Parts);
        }

        private Task<TMDBConfig> GetConfig()
             => _dataService.Retreive<TMDBConfig>(Global.TmdbConfigPath);

        private List<Movie> ConvertToDisukuMovie(List<SearchMovie> movies)
        {
            var result = new List<Movie>();
            foreach (var movie in movies)
            {
                result.Add(new Movie
                {
                    Title = movie.Title,
                    Description = movie.Overview,
                    ImageUrl = $"http://image.tmdb.org/t/p/w500{movie.PosterPath}",
                    Url = $"https://www.themoviedb.org/movie/{movie.Id}",
                    BackdropUrl = $"http://image.tmdb.org/t/p/w500{movie.BackdropPath}",
                    ReleaseDate = movie.ReleaseDate.Value
                });
            }

            return result;
        }

    }
}
