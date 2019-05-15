using DisukuBot.DisukuCore.Entities.TMDB;
using DisukuData;
using DisukuData.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDbLib.Client;
using System.Linq;
using TMDbLib.Objects.Search;
using System;
using DisukuBot.DisukuDiscord.Extensions;
using DisukuBot.DisukuDiscord;

namespace DisukuBot.DisukuCore.Services.TMDB
{
    public class TmdbService : ITmdbService, IServiceExtention
    {
        private IDisukuJsonDataService _dataService;
        private TMDbClient _client;
        private TMDBConfig _config;

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

        public async Task<MovieCollection> GetMovieCollectionAsync(string collectionName)
        {
            var search = await _client.SearchCollectionAsync(collectionName);
            if (search.Results.Count < 1) { return new MovieCollection(); }

            var collection = await _client.GetCollectionAsync(search.Results.First().Id);
            var movies = ConvertToDisukuMovie(collection.Parts);

            return new MovieCollection
            {
                Title = collection.Name,
                Description = collection.Overview,
                ImageUrl = $"http://image.tmdb.org/t/p/w500{collection.BackdropPath}",
                BackdropUrl = $"http://image.tmdb.org/t/p/w500{collection.PosterPath}",
                Movies = movies,
                Url = $"https://www.themoviedb.org/collection/{collection.Id}",
            };
        }

        public async Task<TVShow> GetTvShowAsync(string name)
        {
            var search = await _client.SearchTvShowAsync(name);
            var result = search.Results.First();
            return new TVShow
            {
                Title = result.Name,
                Description = result.Overview,
                Url = $"https://www.themoviedb.org/tv/{result.Id}",
                ImageUrl = $"http://image.tmdb.org/t/p/w500{result.PosterPath}",
                BackdropUrl = $"http://image.tmdb.org/t/p/w500{result.BackdropPath}"
            };
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
