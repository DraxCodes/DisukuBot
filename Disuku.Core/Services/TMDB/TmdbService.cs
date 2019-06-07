using Disuku.Core.Discord;
using Disuku.Core.Entities.Embeds;
using Disuku.Core.Entities.TMDB;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Client;

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

        public async Task ReplyMovieCollectionAsync(ulong chanId, string collectionName)
        {
            var search = await _client.SearchCollectionAsync(collectionName);
            if (search.Results.Count < 1)
            {
                await _discordMessage.SendDiscordMessageAsync(chanId, $"No Results Found for {collectionName}");
                return;
            }

            var collection = await _client.GetCollectionAsync(search.Results.First().Id);

            var embed = new DisukuEmbed
            {
                Title = collection.Name,
                Description = collection.Overview,
                ImageUrl = $"http://image.tmdb.org/t/p/w500{collection.BackdropPath}",
                Thumbnail = $"http://image.tmdb.org/t/p/w500{collection.PosterPath}",
                URL = $"https://www.themoviedb.org/collection/{collection.Id}",
                Footer = $"Collection Size: {collection.Parts.Count}"
            };

            await _discordMessage.SendDiscordEmbedAsync(chanId, embed);
        }

        public async Task ReplyTvShowAsync(ulong chanId, string name)
        {
            var search = await _client.SearchTvShowAsync(name);
            var result = search.Results.First();

            var embed = new DisukuEmbed
            {
                Title = result.Name,
                Description = result.Overview,
                URL = $"https://www.themoviedb.org/tv/{result.Id}",
                Thumbnail = $"http://image.tmdb.org/t/p/w500{result.PosterPath}",
                ImageUrl = $"http://image.tmdb.org/t/p/w500{result.BackdropPath}"
            };

            await _discordMessage.SendDiscordEmbedAsync(chanId, embed);
        }

        private TMDBConfig GetConfig()
        {
            var rawJson = File.ReadAllText(Global.TmdbConfigPath);
            return JsonConvert.DeserializeObject<TMDBConfig>(rawJson);
        }
    }
}
