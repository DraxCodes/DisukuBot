using Discord;
using Discord.Commands;
using Disuku.Core.Services.TMDB;
using System.Threading.Tasks;

namespace Disuku.Discord.Modules
{
    public class TMDB : ModuleBase<SocketCommandContext>
    {
        private ITmdbService _tmdbService;

        public TMDB(ITmdbService tmdbService)
        {
            _tmdbService = tmdbService;
        }

        [Command("Movie")]
        public async Task GetMovie([Remainder]string search)
            => await _tmdbService.ReplyMovieAsync(Context.Channel.Id, search);

        [Command("MCollection")]
        public async Task GetCollection([Remainder]string search)
        {
            var collection = await _tmdbService.GetMovieCollectionAsync(search);
            if (collection.Title == null)
            {
                await ReplyAsync($"Nothing found for: {search}");
                return;
            }
            var embed = new EmbedBuilder()
                .WithTitle(collection.Title)
                .WithDescription(collection.Description)
                .WithThumbnailUrl(collection.BackdropUrl)
                .WithImageUrl(collection.ImageUrl)
                .WithUrl(collection.Url)
                .WithColor(Color.Blue);

            await ReplyAsync(embed: embed.Build());
        }

        [Command("TvShow")]
        public async Task GetTvShow([Remainder]string search)
        {
            var result = await _tmdbService.GetTvShowAsync(search);
            var embed = new EmbedBuilder()
                .WithTitle(result.Title)
                .WithDescription(result.Description)
                .WithColor(Color.Blue)
                .WithUrl(result.Url)
                .WithImageUrl(result.BackdropUrl)
                .WithThumbnailUrl(result.ImageUrl);

            await ReplyAsync(embed: embed.Build());
        }
    }
}
