using Discord;
using Discord.Commands;
using Disuku.Core.Services.TMDB;
using System.Threading.Tasks;

namespace DisukuBot.DisukuDiscord.Modules
{
    public class TMDB : ModuleBase<SocketCommandContext>
    {
        private ITmdbService _tmdbService;
        private string _logo = "https://www.themoviedb.org/assets/2/v4/logos/293x302-powered-by-square-green-3ee4814bb59d8260d51efdd7c124383540fc04ca27d23eaea3a8c87bfa0f388d.png";

        public TMDB(ITmdbService tmdbService)
        {
            _tmdbService = tmdbService;
        }

        [Command("Movie")]
        public async Task GetMovie([Remainder]string search)
        {
            var result = await _tmdbService.GetMovieAsync(search);

            var embed = new EmbedBuilder()
                .WithTitle(result.Title)
                .WithDescription(result.Description)
                .WithColor(Color.Blue)
                .WithUrl(result.Url)
                .WithImageUrl(result.BackdropUrl)
                .WithFooter($"Release Date: {result.ReleaseDate.ToShortDateString()}", _logo)
                .WithThumbnailUrl(result.ImageUrl);

            await ReplyAsync(embed: embed.Build());
        }

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
