﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TMDbLib.Client;
using System.Linq;
using TMDbLib.Objects.Search;
using Disuku.Core.Entities.TMDB;
using System.IO;
using Newtonsoft.Json;
using Disuku.Core.Discord;
using Disuku.Core.Entities.Embeds;

namespace Disuku.Core.Services.TMDB
{
    public class TmdbService : ITmdbService
    {
        private TMDbClient _client;
        private TMDBConfig _config;
        private IDiscordMessage _discordMessage;

        public TmdbService(IDiscordMessage discordMessage)
        {
            _discordMessage = discordMessage;
            _config = GetConfig();
            _client = new TMDbClient(_config.Token);
        }

        public async Task ReplyMovieAsync(ulong chanId, string name)
        {
            var search = await _client.SearchMovieAsync(name);

            var result = search?.Results.First();

            var movieEmbed = new DisukuEmbed
            {
                Title = result.Title,
                Description = result.Overview,
                Thumbnail = $"http://image.tmdb.org/t/p/w500{result.PosterPath}",
                URL = $"https://www.themoviedb.org/movie/{result.Id}",
                ImageUrl = $"http://image.tmdb.org/t/p/w500{result.BackdropPath}",
                Footer = $"Release: {result.ReleaseDate.Value.ToShortDateString()}"
            };

            await _discordMessage.SendDiscordEmbedAsync(chanId, movieEmbed);
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

        private TMDBConfig GetConfig()
        {
            var rawJson = File.ReadAllText(Global.TmdbConfigPath);
            return JsonConvert.DeserializeObject<TMDBConfig>(rawJson);
        }

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
